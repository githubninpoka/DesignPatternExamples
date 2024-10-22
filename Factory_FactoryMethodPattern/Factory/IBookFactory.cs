using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Factory_FactoryMethodPattern.Factory;

public abstract class IBookFactory
{
    protected abstract IBook Create();

    public virtual IBook Make()
    {
        return Create();
    }
}
