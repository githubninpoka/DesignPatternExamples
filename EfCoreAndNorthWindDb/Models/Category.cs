﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreAndNorthWindDb.Models;

public class Category
{
    // These properties map to columns in the database.
    public int CategoryId { get; set; } // The primary key.
    
    public string CategoryName { get; set; } = null!;
    [Column(TypeName = "ntext")]
    public string? Description { get; set; }
    // Defines a navigation property for related rows.
    public virtual ICollection<Product> Products { get; set; }
      // To enable developers to add products to a Category, we must
      // initialize the navigation property to an empty collection.
      // This also avoids an exception if we get a member like Count.
      = new HashSet<Product>();
}
