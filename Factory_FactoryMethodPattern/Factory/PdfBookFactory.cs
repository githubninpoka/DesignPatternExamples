using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_FactoryMethodPattern.Factory;

public class PdfBookFactory : IBookFactory
{
    protected override IBook Create()
    {
        return new PdfBook();
    }
}
