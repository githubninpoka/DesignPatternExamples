using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_FactoryMethodPattern.Factory;

public interface IBook
{
    public string BookText { get; set; }
    public List<string> BookMatches { get; set; }

    public string Title { get; set; }
    public string Author { get; set; }
    public int? NumberOfPages { get; set; }
}
