using System;
using GraphQL.Next.Configs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLScalarType : GraphQLType, IGraphQLInputType, IGraphQLOutputType
    {
        protected GraphQLScalarType(GraphQLScalarTypeConfig config)
        {
            Name = config.Name;
            Description = config.Description;
            DeprecationReason = config.DeprecationReason;
        }

        public string Description { get; protected set; }
        public string DeprecationReason { get; protected set; }

        public static GraphQLScalarType For(Action<GraphQLScalarTypeConfig> configure)
        {
            var config = new GraphQLScalarTypeConfig();
            configure(config);
            return new GraphQLScalarType(config);
        }
    }

    public class GraphQLScalarType<TType> : GraphQLScalarType
    {
        public GraphQLScalarType(GraphQLScalarTypeConfig config)
            : base(config)
        {
        }

        public static GraphQLScalarType<TType> For(Action<GraphQLScalarTypeConfig<TType>> configure)
        {
            var config = new GraphQLScalarTypeConfig<TType>();
            configure(config);
            return new GraphQLScalarType<TType>(config);
        }
    }
}
