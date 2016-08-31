using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public interface IGraphQLInterfaceTypeConfig : IGraphQLAbstractTypeConfig
    {
        IEnumerable<GraphQLFieldDefinition> Fields { get; }
    }

    public class GraphQLInterfaceTypeConfig : GraphQLComplexTypeConfig, IGraphQLInterfaceTypeConfig
    {
        public Func<object, GraphQLType> ResolveType { get; set; }
    }

    public class GraphQLInterfaceTypeConfig<TModel> : GraphQLComplexTypeConfig<TModel>, IGraphQLInterfaceTypeConfig
    {
        public Func<object, GraphQLType> ResolveType { get; set; }
    }
}
