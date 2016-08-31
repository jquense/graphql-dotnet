using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLInputObjectType : GraphQLType, IGraphQLInputType
    {
        private readonly List<GraphQLInputObjectField> _fields = new List<GraphQLInputObjectField>();

        public GraphQLInputObjectType(GraphQLInputObjectTypeConfig config)
        {
            Name = config.Name;
            Description = config.Description;
            _fields.AddRange(config.Fields);
        }

        public string Description { get; set; }

        public IEnumerable<GraphQLInputObjectField> Fields => _fields;

        public static GraphQLInputObjectType For(Action<GraphQLInputObjectTypeConfig> configure)
        {
            var config = new GraphQLInputObjectTypeConfig();
            configure(config);
            return new GraphQLInputObjectType(config);
        }

        public static GraphQLInputObjectType For<T>(Action<GraphQLInputObjectTypeConfig<T>> configure)
        {
            var config = new GraphQLInputObjectTypeConfig<T>();
            configure(config);
            return new GraphQLInputObjectType(config);
        }
    }
}
