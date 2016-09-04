using GraphQL.Resolvers;
using System;
using System.Collections.Generic;

namespace GraphQL.Types
{
    public interface IComplexFieldType: IFieldType
    {
        IEnumerable<IFieldType> Fields { get; set; }
    }
}
