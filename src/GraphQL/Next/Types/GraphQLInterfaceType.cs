using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static GraphQLInterfaceType For(Action<GraphQLInterfaceTypeConfig> configure)
        {
            var config = new GraphQLInterfaceTypeConfig();
            configure(config);
            return new GraphQLInterfaceType(config);
        }
    }

    public class GraphQLInterfaceType<TModel> : GraphQLInterfaceType
    {
        public GraphQLInterfaceType(GraphQLInterfaceTypeConfig<TModel> config)
            : base(config)
        {
        }

        public static GraphQLInterfaceType<TModel> For(Action<GraphQLInterfaceTypeConfig<TModel>> configure)
        {
            var type = typeof(TModel);

            var config = new GraphQLInterfaceTypeConfig<TModel>();
            var name = type.Name;

            if (type.IsInterface && name.StartsWith("I"))
            {
                name = name.Substring(1);
            }

            config.Name = name;

            configure(config);
            return new GraphQLInterfaceType<TModel>(config);
        }
    }
}
