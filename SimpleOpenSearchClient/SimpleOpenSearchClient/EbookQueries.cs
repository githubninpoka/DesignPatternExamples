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
        MatchQuery shiningPathsMatch = new MatchQuery()
        {
            Field = "fileName",
            Query = "shining",
            //Fuzziness = Fuzziness.Auto // works but is not yet needed
        };
        shiningPathsMatch.Fuzziness = Fuzziness.Auto;

        MatchQuery elementaireDeeltjesMatch = new MatchQuery()
        {
            Field = "filePath",
            Query = "elementaireDeeltjes"
        };

        MatchQuery textMatch = new MatchQuery()
        {
            Field = "bookText",
            Query = "niets"
        };
        BoolQuery boolQuery = new BoolQuery()
        {
            // works yay!
            //Should = [elementaireDeeltjesMatch,shiningPathsMatch],
            //MinimumShouldMatch = 1, 
            Must = [textMatch]
            
            
        };
        boolQuery.Should = [elementaireDeeltjesMatch, shiningPathsMatch];
        //boolQuery.MinimumShouldMatch = 1;

        //Field[] includedSourceFields = new Field[5];
        //includedSourceFields[0] = new Field("title");
        //includedSourceFields[1] = new Field("pages");
        //includedSourceFields[2] = new Field("author");
        //includedSourceFields[3] = new Field("filePath");
        //includedSourceFields[4] = new Field("fileName");
        List<Field> includedSourceFields = new();
        includedSourceFields.Add(new Field("title"));
        includedSourceFields.Add(new Field("fileName"));
        


        SourceFilter sf = new SourceFilter()
        {
            Includes = includedSourceFields.ToArray()
        };

        var query = new SearchRequest("ebooks")
        {
            // query can be many types but in this case it is a simple MatchQuery
            //Query = match

            // works yay!
            Query = boolQuery,
            Size = 20,
            
            

        };
        query.Source = sf;
        query.Size = 5;


        var response = await client.SearchAsync<BookMetaData>(query);
        Console.WriteLine("waiting for enter");
        Console.ReadLine();

    }
}


