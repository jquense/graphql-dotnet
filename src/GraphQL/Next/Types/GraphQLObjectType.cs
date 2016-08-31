using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public interface IGraphQLObjectTypeConfig : IGraphQLComplexTypeConfig
    {
        string DeprecationReason { get; set; }
        Func<object, bool> IsOfType { get; set; }
        IEnumerable<GraphQLInterfaceType> Interfaces { get; }
        void Interface(params GraphQLInterfaceType[] interfaces);
    }


    public class GraphQLObjectType : GraphQLType, IGraphQLOutputType
    {
        private readonly Dictionary<string, GraphQLFieldDefinition> _fields =
            new Dictionary<string, GraphQLFieldDefinition>(StringComparer.OrdinalIgnoreCase);
        private readonly List<GraphQLInterfaceType> _interfaces = new List<GraphQLInterfaceType>();

        public GraphQLObjectType()
        {
        }

        public GraphQLObjectType(IGraphQLObjectTypeConfig config)
        {
            Initialize(config);
        }

        public string Description { get; protected set; }
        public string DeprecationReason { get; protected set; }
        public Func<object, bool> IsOfType { get; protected set; }

        public void Initialize(IGraphQLObjectTypeConfig config)
        {
            Name = config.Name;
            Description = config.Description;
            DeprecationReason = config.DeprecationReason;
            Fields = config.Fields;
            Interfaces = config.Interfaces;
            IsOfType = config.IsOfType;

            Invariant.Check(
                !string.IsNullOrWhiteSpace(Name),
                "Type must be named.");

            if (Interfaces != null && Interfaces.Any())
            {
                Invariant.Check(
                    IsOfType != null,
                    $"{Name} does not provide a \"isTypeOf\" function.  There is no way to resolve this "
                    + "implementing type during execution.");
            }
        }

        public GraphQLFieldDefinition FieldFor(string name)
        {
            GraphQLFieldDefinition field;

            _fields.TryGetValue(name, out field);

            return field;
        }

        public IEnumerable<GraphQLFieldDefinition> Fields
        {
            get { return _fields.Values; }
            protected set
            {
                _fields.Clear();
                value.Apply(f => _fields[f.Name] = f);
            }
        }

        public IEnumerable<GraphQLInterfaceType> Interfaces
        {
            get { return _interfaces; }
            set
            {
                _interfaces.Clear();
                _interfaces.AddRange(value);
            }
        }

        public static GraphQLObjectType For(Action<GraphQLObjectTypeConfig> configure)
        {
            var config = new GraphQLObjectTypeConfig();
            configure(config);
            return new GraphQLObjectType(config);
        }
    }

    public class GraphQLObjectType<TModel> : GraphQLObjectType
    {
        public GraphQLObjectType()
        {
        }

        public GraphQLObjectType(GraphQLObjectTypeConfig<TModel> config)
            : base(config)
        {
        }

        public static GraphQLObjectType<TModel> For(Action<GraphQLObjectTypeConfig<TModel>> configure)
        {
            var type = typeof(TModel);

            var config = new GraphQLObjectTypeConfig<TModel>();
            var name = type.Name;

            if (type.IsInterface && name.StartsWith("I"))
            {
                name = name.Substring(1);
            }

            config.Name = name;

            configure(config);
            return new GraphQLObjectType<TModel>(config);
        }
    }
}
