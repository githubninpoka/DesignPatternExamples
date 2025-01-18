using Microsoft.Extensions.Configuration;
using OpenSearch.Client;
using System.ComponentModel;
using System.Net;

namespace SimpleOpenSearchClient;

internal class Program
{
    // a simple project to do basic operations on OpenSearch (in my docker locally)
    // it connects, creates an index after deleting it first, inserts some random data
    // and fetches the documents.
    // the last query checks for the existence of a value without fetching the whole document.


    private static string user = "admin";
    private static string pass = "pass";
    static async Task Main(string[] args)
    {
        ConfigurationBuilder configBuilder = new ConfigurationBuilder();
        configBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configBuilder.AddJsonFile("appsettings.json");
        configBuilder.AddUserSecrets<Program>();
        IConfiguration appConfig = configBuilder.Build();
        pass = appConfig.GetSection("pass").Value;
        Console.WriteLine($"Using password {pass}");

        List<Whever> wheverList = new();
        for (int i = 0; i < 100; i++)
        {
            wheverList.Add(new Whever { Number = i, Text = $"This is a text for item {i}" });
        }
        Whever[] wheverArray = wheverList.ToArray();

        var node = new Uri("https://127.0.0.1:9200");
        var config = new ConnectionSettings(node);
        config.BasicAuthentication(user, pass);
        config.DefaultIndex("whever");
        config.ServerCertificateValidationCallback((o, c, ch, er) => true); // stupid visual studio doesn't want to trust a self signed certificate
        
        //config.EnableDebugMode();
        
        config.OnRequestCompleted(callDetails =>
        {
            Console.WriteLine($"Request completed: {callDetails.HttpMethod} {callDetails.Uri}");
            if (callDetails.ResponseBodyInBytes != null)
            {
                var responseBody = System.Text.Encoding.UTF8.GetString(callDetails.ResponseBodyInBytes);
                Console.WriteLine($"Response: {responseBody}");
            }
        });
        var client = new OpenSearchClient(config);


        var existResponse = await client.Indices.ExistsAsync("whever");
        if (existResponse.Exists)
        {
            var deleteResponse = client.Indices.Delete("whever");
        }
        else
        {
            Console.WriteLine(existResponse.DebugInformation);
        }
        var createResponse = await client.Indices.CreateAsync("whever", c => c.Map(m => m.AutoMap<Whever>()));
        
        var response = client.IndexMany<Whever>(wheverArray);
        Console.WriteLine(!response.IsValid ? response.OriginalException.Message.ToString() : response.ToString());
       

        await Task.Delay(3000);

        //var one = client.Search<Whever>(x => x.Query(
        //    q => q.Match(m => m.Field(fld => fld.Text)
        //    .Query("text")
        //    .Operator(Operator.And))));
        //var two = client.Search<Whever>(x => x.Query(
        //    q => q.Match(
        //        m => m.Field(
        //            fld => fld.Text)
        //        .Query("text"))));
        //var three = client.Search<Whever>(x => x
        //.Query(
        //    q => q.MatchPhrase(
        //        m => m.Field(
        //            f => f.Text)
        //        .Query("text")
        //        )
        //    )
        //.Take(50)
        //);
        //foreach (var item in three.Documents)
        //{
        //    Console.WriteLine($"{item.Number} has a value: {item.Text}");
        //}
        //
        //// the below .Source(false) excludes the object from the search. According to what I //could find in the documentation
        //// there is no better way to check for the existence of a value in a field.
        //// this search returns (in this case) a document set with one element which is null. //when searching for a non existent value
        //// the document result set itself is null. so if document.count>0 there is a match.
        //var four = client.Search<Whever>(s => s
        //.Source(false)
        //.Query(q => q
        //.Term(t => t
        //.Field("number")
        //.Value("3"))));
        //
        //var five = client.Search<Whever2>(x => x
        //.Query(
        //    q => q.MatchPhrase(
        //        m => m.Field(
        //            f => f.Text)
        //        .Query("text")
        //        )
        //    )
        //.Take(50)
        //);

        //EbookQueries.BoolQuery(client, true);
        EbookQueries.BoolQueryWithQueryObject(client);

        Console.ReadLine();


    }
}

public class Whever
{
    public int Number { get; set; }
    public string Text { get; set; }
}

public class Whever2
{
    public string Text { get; set; }
}