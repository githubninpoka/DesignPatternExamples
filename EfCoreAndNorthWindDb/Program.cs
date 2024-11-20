using EfCoreAndNorthWindDb.DataAccess;
using EfCoreAndNorthWindDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreAndNorthWindDb;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new NorthwindDb();
        Console.WriteLine(db.Database.ProviderName);
        Console.WriteLine(db.Categories.FirstOrDefault().CategoryName);
        //QueryingProducts();
        //QueryingWithLike();
        //GetProductUsingSql();
        //JoinCategoriesAndProductsOrdered();
        //GroupJoinCategoriesAndProducts();
        ProductsLookup();
    }

    private static void QueryingProducts()
    {
        using NorthwindDb db = new();
        Console.WriteLine("Products that cost more than a price, highest at top");
        string? input;
        decimal price;
        do
        {
            Console.WriteLine("Enter a product price: ");
            input = Console.ReadLine();
        } while (!decimal.TryParse(input, out price));
        IQueryable<Product>? products = db.Products?
          .Where(product => product.Cost > price)
          .OrderByDescending(product => product.Cost);
        if (products is null || !products.Any())
        {
            Console.WriteLine("None found");
            return;
        }
        //Console.WriteLine("Query executed:");
        //Console.WriteLine(products.ToQueryString()); // works only on Iqueryables
        foreach (Product p in products)
        {
            Console.WriteLine(
              "{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
              p.ProductId, p.ProductName, p.Cost, p.Stock);
        }
    }
    private static void QueryingWithLike()
    {
        using NorthwindDb db = new();
        Console.WriteLine("Pattern matching with LIKE");
        Console.WriteLine("Enter part of a product name: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("You did not enter part of a product name.");
            return;
        }
        IQueryable<Product>? products = db.Products?
          .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
        if (products is null || !products.Any())
        {
            Console.WriteLine("No products found.");
            return;
        }
        foreach (Product p in products)
        {
            Console.WriteLine("{0} has {1} units in stock. Discontinued: {2}",
              p.ProductName, p.Stock, p.Discontinued);
        }
    }

    private static void GetProductUsingSql()
    {
        using NorthwindDb db = new();
        Console.WriteLine("Get product using SQL");
        int? rowCount = db.Products?.Count();
        if (rowCount is null)
        {
            Console.WriteLine("Products table is empty.");
            return;
        }
        int productId = 1;
        Product? p = db.Products?.FromSql(
          $"SELECT * FROM Products WHERE ProductId = {productId}").FirstOrDefault();
        if (p is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        Console.WriteLine($"Product: {p.ProductId} - {p.ProductName}");
    }

    private static void JoinCategoriesAndProductsOrdered()
    {
        Console.WriteLine("Join categories and products");
        using NorthwindDb db = new();
        // Join every product to its category to return 77 matches.
        var queryJoin = db.Categories.Join(
          inner: db.Products,
          outerKeySelector: category => category.CategoryId,
          innerKeySelector: product => product.CategoryId,
          resultSelector: (c, p) =>
            new { c.CategoryName, p.ProductName, p.ProductId })
            .OrderBy(cp => cp.CategoryName); 
        foreach (var p in queryJoin)
        {
            Console.WriteLine($"{p.ProductId}: {p.ProductName} in {p.CategoryName}.");
        }
    }

    private static void GroupJoinCategoriesAndProducts()
    {
        Console.WriteLine("Group join categories and products");
        using NorthwindDb db = new();
        // Group all products by their category to return 8 matches.
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(
          inner: db.Products,
          outerKeySelector: category => category.CategoryId,
          innerKeySelector: product => product.CategoryId,
          resultSelector: (c, matchingProducts) => new
          {
              c.CategoryName,
              Products = matchingProducts.OrderBy(p => p.ProductName)
          });
        foreach (var c in queryGroup)
        {
            Console.WriteLine($"{c.CategoryName} has {c.Products.Count()} products.");
            foreach (var product in c.Products)
            {
                Console.WriteLine($"  {product.ProductName}");
            }
        }
    }

    private static void ProductsLookup()
    {
        Console.WriteLine("Products lookup");
        using NorthwindDb db = new();
        // Join all products to their category to return 77 matches.
        var productQuery = db.Categories.Join(
          inner: db.Products,
          outerKeySelector: category => category.CategoryId,
          innerKeySelector: product => product.CategoryId,
          resultSelector: (c, p) => new { c.CategoryName, Product = p });
        ILookup<string, Product> productLookup = productQuery.ToLookup(
          keySelector: cp => cp.CategoryName,
          elementSelector: cp => cp.Product);
        foreach (IGrouping<string, Product> group in productLookup)
        {
            // Key is Beverages, Condiments, and so on.
            Console.WriteLine($"{group.Key} has {group.Count()} products.");
            foreach (Product product in group)
            {
                Console.WriteLine($" {product.ProductName}");
            }
        }
        // We can look up the products by a category name.
        Console.WriteLine("Enter a category name: ");
        string categoryName = Console.ReadLine()!;
        Console.WriteLine();
        Console.WriteLine($"Products in {categoryName}:");
        IEnumerable<Product> productsInCategory = productLookup[categoryName];
        foreach (Product product in productsInCategory)
        {
            Console.WriteLine($"  {product.ProductName}");
        }
    }

}
