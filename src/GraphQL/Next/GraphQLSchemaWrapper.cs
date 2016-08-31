using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public class GraphQLSchemaWrapper
    {
        private readonly Dictionary<string, IGraphQLType> _types;
        private object _schema;

        public GraphQLSchemaWrapper(object schema)
        {
            _schema = schema;

            Query = GetProperty("query", schema);
            Mutation = GetProperty("mutation", schema);

            Invariant.Check(
                Query != null || Mutation != null,
                "A Query or Mutation property of type GraphQLObjectType is required.");

            var walker = new GraphQLTreeWalker();
            _types = walker.Walk(this);
        }

        public GraphQLObjectType Query { get; }
        public GraphQLObjectType Mutation { get; }

        public IGraphQLType TypeFor(string name)
        {
            var normalized = name.NormalizeTypeName();

            IGraphQLType type;

            _types.TryGetValue(normalized, out type);

            Invariant.Check(type != null, $"Unknown requested type '{normalized}'.");

            return type;
        }

        private GraphQLObjectType GetProperty(string name, object schema)
        {
            var type = schema.GetType();
            var info = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return info?.GetValue(schema, null) as GraphQLObjectType;
        }
    }
}
