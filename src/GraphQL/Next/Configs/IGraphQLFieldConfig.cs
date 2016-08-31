using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public interface IGraphQLFieldConfig
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
