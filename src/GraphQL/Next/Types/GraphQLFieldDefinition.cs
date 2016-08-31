using GraphQL.Next.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLFieldDefinition
    {
        public GraphQLFieldDefinition()
        {
            Arguments = new GraphQLArgument[0];
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string DeprecationReason { get; set; }
        public IGraphQLOutputType Type { get; set; }
        public IEnumerable<GraphQLArgument> Arguments { get; set; }
        public IFieldResolver Resolver { get; set; }
    }
}
