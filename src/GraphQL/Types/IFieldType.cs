using GraphQL.Resolvers;
using System;

namespace GraphQL.Types
{
    public interface IFieldType: IHaveDefaultValue
    {
        string Name { get; }

        string Description { get; }

        string DeprecationReason { get; }

        QueryArguments Arguments { get; }

        IFieldResolver Resolver { get; }
    }
}
