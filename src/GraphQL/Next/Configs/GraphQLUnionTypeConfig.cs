using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLUnionTypeConfig : IGraphQLAbstractTypeConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Func<object, GraphQLType> ResolveType { get; set; }
        public GraphQLObjectType[] Types { get; set; }
    }
}
