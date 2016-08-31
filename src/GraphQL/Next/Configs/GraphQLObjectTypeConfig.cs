using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLObjectTypeConfigBase
    {
    }

    public class GraphQLObjectTypeConfig : GraphQLTypeFieldConfig, IGraphQLObjectTypeConfig
    {
        private readonly List<GraphQLInterfaceType> _interfaces = new List<GraphQLInterfaceType>();

        public string DeprecationReason { get; set; }
        public Func<object, bool> IsOfType { get; set; }

        public IEnumerable<GraphQLInterfaceType> Interfaces => _interfaces;

        public void Interface(params GraphQLInterfaceType[] interfaces)
        {
            _interfaces.AddRange(interfaces);
        }
    }

    public class GraphQLObjectTypeConfig<T> : GraphQLTypeFieldConfig<T>, IGraphQLObjectTypeConfig
    {
        private readonly List<GraphQLInterfaceType> _interfaces = new List<GraphQLInterfaceType>();

        public string Name { get; set; }
        public string Description { get; set; }
        public string DeprecationReason { get; set; }
        public Func<object, bool> IsOfType { get; set; }

        public IEnumerable<GraphQLInterfaceType> Interfaces => _interfaces;

        public void Interface(params GraphQLInterfaceType[] interfaces)
        {
            _interfaces.AddRange(interfaces);
        }
    }
}
