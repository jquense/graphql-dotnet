using GraphQL.Resolvers;
using System;
using System.Collections.Generic;

namespace GraphQL.Types
{
    public interface IAbstractFieldType: IFieldType
    {
        IEnumerable<IFieldType> PossibleTypes { get; set; }
    }
}
