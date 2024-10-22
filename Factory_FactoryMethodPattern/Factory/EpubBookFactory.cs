using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_FactoryMethodPattern.Factory;

internal class EpubBookFactory : IBookFactory
{
    protected override IBook Create()
    {
        return new EpubBook();
    }
}
