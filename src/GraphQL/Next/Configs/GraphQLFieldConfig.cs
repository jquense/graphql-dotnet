using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLFieldConfig : GraphQLArgumentConfig, IGraphQLFieldConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeprecationReason { get; set; }
        public IGraphQLOutputType Type { get; set; }
        public IFieldResolver Resolve { get; set; }
    }
}
