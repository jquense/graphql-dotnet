using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{   
    public interface IGraphQLTypeConfig
    {
        string Name { get; set; }
        string Description { get; set; }
        string DeprecationReason { get; set; }
    }

    public class GraphQLTypeConfig : IGraphQLTypeConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeprecationReason { get; set; }
    }
}
