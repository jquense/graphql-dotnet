using GraphQL.Resolvers;
using System;

namespace GraphQL.Types
{
    public interface IOutputType
    {
    }

    public interface IInputType
    {
    }

    public interface ITypeDefinition : IHaveDefaultValue
    {
        string Name { get; }

        string Description { get; }

        string DeprecationReason { get; }

        QueryArguments Arguments { get; }

        IFieldResolver Resolver { get; }
    }
}
