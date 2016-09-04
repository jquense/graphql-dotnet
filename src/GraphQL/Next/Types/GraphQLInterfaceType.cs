using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Next.Builders;

namespace GraphQL.Next.Types
{
    public class GraphQLInterfaceType : GraphQLAbstractType
    {
        private readonly List<GraphQLFieldDefinition> _fields = new List<GraphQLFieldDefinition>();

        public GraphQLInterfaceType()
        {
        }

        public GraphQLInterfaceType(IGraphQLInterfaceTypeConfig config)
        {
            Initialize(config);
        }

        public string DeprecationReason { get; protected set; }

        public IEnumerable<GraphQLFieldDefinition> Fields
        {
            get { return _fields; }
            protected set
            {
                _fields.Clear();
                _fields.AddRange(value);
            }
        }

        public void Initialize(IGraphQLInterfaceTypeConfig config)
        {
            base.Initialize(config);
            Fields = config.Fields;
        }

        public static GraphQLInterfaceType For<TSourceType>(Action<InterfaceBuilder<TSourceType>> configure)
        {
            var builder = new InterfaceBuilder<TSourceType>(new GraphQLInterfaceTypeConfig()
            {
                Name = typeof(TSourceType).GetTypeNameFromType()
            });

            configure(builder);

            return new GraphQLInterfaceType(builder.Resolve());
        }
    }
}
