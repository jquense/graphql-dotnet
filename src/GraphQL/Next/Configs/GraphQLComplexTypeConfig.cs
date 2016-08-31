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
    public class GraphQLComplexTypeConfig : GraphQLTypeConfig, IGraphQLComplexTypeConfig
    {
        private readonly List<GraphQLFieldDefinition> _fields = new List<GraphQLFieldDefinition>();
        public IEnumerable<GraphQLFieldDefinition> Fields => _fields;

        public void AddField(GraphQLFieldDefinition field)
        {
            _fields.Add(field);
        }
    }

    public class GraphQLComplexTypeConfig<TSourceType> : GraphQLComplexTypeConfig
    {

    }
}
