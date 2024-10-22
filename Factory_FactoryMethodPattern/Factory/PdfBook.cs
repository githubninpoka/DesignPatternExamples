using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_FactoryMethodPattern.Factory;

internal class PdfBook : IBook
{
    public string BookText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public List<string> BookMatches { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int? NumberOfPages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
