// What I am really looking for is a fuzzy search like the one implemented in Opensearch or (SOUNDEX) in SQL server, but 
// I don't know yet if Fuzzy String / Fuzzy Search libraries for C# return the index at which they were found.
// something like:
// FuzzyMatches fuzzyMatches = FuzzySearch.FindMatches(textThatNeedsToBeSearchedThrough, patternOrStringThatNeedsToBeFuzzyFound);
// and then the resulting FuzzyMatch contains the value but also the index at which it was found. if that exists, then I'm golden.

using System.Globalization;
using System.Text.RegularExpressions;

//using FuzzySharp; // old package
//using FuzzySharp.SimilarityRatio.Scorer; // old package

using Raffinert.FuzzySharp;
using Raffinert.FuzzySharp.SimilarityRatio.Scorer;
using RegexAndSimpleFuzzy;
using static System.Formats.Asn1.AsnWriter;
//using DuoVia.FuzzyStrings;
//using GSF.FuzzyStrings;

Console.WriteLine("exact match");
string pattern = "Marco";
Console.WriteLine(Regex.IsMatch("Marco",pattern));
Console.WriteLine(Regex.IsMatch("Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("De Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("marco", pattern));
Console.WriteLine(Regex.IsMatch("DeMarco", pattern));

Console.WriteLine("Ffirst letter case");
pattern = "[Mm]arco";
Console.WriteLine(Regex.IsMatch("Marco", pattern));
Console.WriteLine(Regex.IsMatch("Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("De Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("marco", pattern));
Console.WriteLine(Regex.IsMatch("DeMarco", pattern));

pattern = "MaRcO";
Console.WriteLine($"Entirely case insensitive ${pattern}");
Console.WriteLine(Regex.IsMatch("Marco", pattern,RegexOptions.IgnoreCase));
Console.WriteLine(Regex.IsMatch("Marco Uitendaal", pattern, RegexOptions.IgnoreCase));
Console.WriteLine(Regex.IsMatch("De Marco Uitendaal", pattern, RegexOptions.IgnoreCase));
Console.WriteLine(Regex.IsMatch("marco", pattern, RegexOptions.IgnoreCase));
Console.WriteLine(Regex.IsMatch("DeMarco", pattern, RegexOptions.IgnoreCase));

Console.WriteLine("Starts with Marco");
pattern = "^Marco";
Console.WriteLine(Regex.IsMatch("Marco", pattern));
Console.WriteLine(Regex.IsMatch("Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("De Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("marco", pattern));
Console.WriteLine(Regex.IsMatch("DeMarco", pattern));

Console.WriteLine("Marco preceded by a whitespace character");
pattern = @"\sMarco"; // @ for using escape sequences
Console.WriteLine(Regex.IsMatch("Marco", pattern));
Console.WriteLine(Regex.IsMatch("Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("De Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("   Marco", pattern));
Console.WriteLine(Regex.IsMatch("DeMarco", pattern));

Console.WriteLine("Marco preceded by a whitespace character or at the beginning or at the end");
pattern = @"(\s|^)Marco(\s|$)"; // @ for using escape sequences
Console.WriteLine(Regex.IsMatch("Marco", pattern));
Console.WriteLine(Regex.IsMatch("Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("De Marco Uitendaal", pattern));
Console.WriteLine(Regex.IsMatch("   Marco", pattern));
Console.WriteLine(Regex.IsMatch("DeMarco", pattern));
Console.WriteLine(Regex.IsMatch("DeMarcoMM", pattern));

string text = "Marco Marco Marco de Parco marco de Parco";
string findPattern = "[mM]arco";
MatchCollection matches = Regex.Matches(text, findPattern);
foreach(Match match in matches)
{
    Console.WriteLine($"At {match.Index} found {match.Value}");
}

Console.WriteLine("One fuzzy attempt");
text = "Well well, here we are";
string find = "hier";
Console.WriteLine(text);
Console.WriteLine(find);
Console.WriteLine(Fuzz.Ratio(text, find));
text = "here we are";
find = "hier";
Console.WriteLine(text);
Console.WriteLine(find);
Console.WriteLine(Fuzz.Ratio(text, find));
text = "here";
find = "hier";
Console.WriteLine(text);
Console.WriteLine(find);
Console.WriteLine(Fuzz.Ratio(text, find));

string[] x = ["sandalphon", "well", "well", "here", "shier", "we", "are"];
find = "hier";


var matchResult = Process.ExtractOne("testString", new List<string> { "testString1", "testString2" });
Console.WriteLine($"Best match: {matchResult}");

var anotherTest = Fuzz.Ratio("Sandalphon", "Sandalfon");

var trialRun = Process.ExtractAll(find, x);
foreach (var item in trialRun)
{
    if (item.Score > 50)
    {
        Console.WriteLine($"Probable match {item.Score} at {item.Index}, namely {item.Value}");
    }
}

//second try
Console.WriteLine("Get me all that have a hit over 50");
find = "hier";
const int CUTOFF= 50;
trialRun = Process.ExtractAll(find, x,cutoff:CUTOFF);
foreach (var item in trialRun)
{
        Console.WriteLine($"Probable match {item.Score} at {item.Index}, namely {item.Value}");
}
//third try
Console.WriteLine("Max hits is one. get me the top one:");
find = "hier";
const int MAXHITS = 1;
trialRun = Process.ExtractTop(find, x, limit:MAXHITS);
foreach (var item in trialRun)
{
    Console.WriteLine($"Probable match {item.Score} at {item.Index}, namely {item.Value}");
}

//fourth try
Console.WriteLine("Fourth try, with sorted and cutoff at 50");
find = "hier";
trialRun = Process.ExtractSorted(find, x,cutoff:CUTOFF);
foreach (var item in trialRun)
{
    Console.WriteLine($"Probable match {item.Score} at {item.Index}, namely {item.Value}");
}



// now with duovia-fuzzystrings
string toFind = "sandalhon";
//bool isEqual = x[0].FuzzyEquals(toFind);
//var howEqual = x[0].FuzzyMatch(toFind);
//bool areSimilar = x[0].FuzzyEquals(toFind);
//Console.WriteLine($"Is {x[0]} equal to {toFind}: {isEqual}, score {howEqual}");

// now with package GSF.Core
//bool areSimilar = x[0].ApproximatelyEquals(toFind, FuzzyStringComparisonOptions.UseLevenshteinDistance, FuzzyStringComparisonTolerance.Normal);
//Console.WriteLine($"Is {x[0]} equal to {toFind}: {areSimilar}");

// now just some matches against a certain pattern.
Console.WriteLine(StringChecker.IsMatchForPattern("banaan"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba naan"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba\"naan"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba[naan"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba{naan"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba(naan"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba3naan"));
Console.WriteLine(StringChecker.IsMatchForPattern("Da'ath"));
Console.WriteLine(StringChecker.IsMatchForPattern("ba   naan"));
