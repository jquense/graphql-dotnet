using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public interface IGraphQLComplexTypeConfig : IGraphQLFieldConfig
    {
        IEnumerable<GraphQLFieldDefinition> Fields { get; }
    }
}
