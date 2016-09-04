using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLComplexTypeConfig : GraphQLTypeConfig
    {
        private readonly Dictionary<string, GraphQLFieldDefinition> _fields = 
            new Dictionary<string, GraphQLFieldDefinition>();

        public IEnumerable<GraphQLFieldDefinition> Fields => _fields.Values;

        public void AddField(GraphQLFieldDefinition field)
        {
            _fields.Add(field.Name, field);
        }

        public GraphQLFieldDefinition FieldFor(string name)
        {
            GraphQLFieldDefinition field;

            _fields.TryGetValue(name, out field);

            return field;
        }
    }

    public class GraphQLComplexTypeConfig<TSourceType> : GraphQLComplexTypeConfig
    {

    }
}
