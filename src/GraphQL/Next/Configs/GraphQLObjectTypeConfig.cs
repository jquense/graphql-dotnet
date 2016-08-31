using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLObjectTypeConfig : GraphQLComplexTypeConfig, IGraphQLObjectTypeConfig
    {
        private readonly List<GraphQLInterfaceType> _interfaces = new List<GraphQLInterfaceType>();

        public Func<object, bool> IsOfType { get; set; }

        public IEnumerable<GraphQLInterfaceType> Interfaces => _interfaces;

        public void Interface(params GraphQLInterfaceType[] interfaces)
        {
            _interfaces.AddRange(interfaces);
        }
    }

    public class GraphQLObjectTypeConfig<T> : GraphQLComplexTypeConfig<T>, IGraphQLObjectTypeConfig
    {
        private readonly List<GraphQLInterfaceType> _interfaces = new List<GraphQLInterfaceType>();

        public Func<object, bool> IsOfType { get; set; }

        public IEnumerable<GraphQLInterfaceType> Interfaces => _interfaces;

        public void Interface(params GraphQLInterfaceType[] interfaces)
        {
            _interfaces.AddRange(interfaces);
        }
    }
}
