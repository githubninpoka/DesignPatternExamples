using OpenSearch.Client;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOpenSearchClient;

internal static class EbookQueries
{
    internal static async Task BoolQuery(OpenSearchClient client, bool toggle)
    {
        var response = await client
            .SearchAsync<BookMetaData>(search => search
                .Index("ebooks")
                .Query(query => query
                    .Match(match => match
                        .Field(field => field.FileName)
                            .Query("shining")
                                //.Fuzziness(Fuzziness.Auto)
                        )
                    )
                            

            );
        Console.WriteLine("waiting for enter");
        Console.ReadLine();
    }
    internal static async Task BoolQueryWithQueryObject(OpenSearchClient client)
    {
        MatchQuery match = new MatchQuery()
        {
            Field = "fileName",
            Query = "shining",
            //Fuzziness = Fuzziness.Auto // works but is not yet needed
        };

        MatchQuery elementaireDeeltjesMatch = new MatchQuery()
        {
            Field = "filePath",
            Query = "elementaireDeeltjes"
        };

        MatchQuery textMatch = new MatchQuery()
        {
            Field = "bookText",
            Query = "bananen"
        };
        BoolQuery boolQuery = new BoolQuery()
        {
            // works yay!
            Should = [elementaireDeeltjesMatch,textMatch],
            MinimumShouldMatch = 1, 
            //Must = textMatch
            
            
        };

        var query = new SearchRequest("ebooks")
        {
            // query can be many types but in this case it is a simple MatchQuery
            //Query = match

            // works yay!
            Query = boolQuery,
            Size = 20

        };

        var response = await client.SearchAsync<BookMetaData>(query);
        Console.WriteLine("waiting for enter");
        Console.ReadLine();

    }
}


