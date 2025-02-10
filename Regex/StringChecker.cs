using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexAndSimpleFuzzy;

public static class StringChecker
{
    public static bool IsMatchForPattern(string term)
    {
        string toDeny = @"[\s,_,\\,"",\{,\},\[,\],\(,\)]";
        Match match = Regex.Match(term, toDeny);
        if (match.Success)
        {
            return false;
        }
        return true;
    }


}
