using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Builders
{
    internal interface IBuilder<T>
    {
        T Resolve();
    }
}
