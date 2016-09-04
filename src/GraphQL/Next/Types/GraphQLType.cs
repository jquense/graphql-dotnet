using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public interface IGraphQLType
    {
        IGraphQLTypeConfig TypeConfig { get; }

        string Name { get; }
        string Description { get; }
        string DeprecationReason { get; }
    }

    public interface IGraphQLOutputType : IGraphQLType
    {
    }

    public interface IGraphQLInputType : IGraphQLType
    {
    }

    public abstract class GraphQLType : IGraphQLType
    {
        public IGraphQLTypeConfig TypeConfig { get; protected set; }

        public string Name => TypeConfig.Name;
        public string Description => TypeConfig.Description;
        public string DeprecationReason => TypeConfig.DeprecationReason;


        internal virtual void Initialize(IGraphQLTypeConfig config)
        {
            TypeConfig = config;

            Invariant.Check(
                !string.IsNullOrWhiteSpace(TypeConfig.Name),
                "Type must be named.");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
