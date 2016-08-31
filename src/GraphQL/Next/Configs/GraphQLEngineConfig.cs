using GraphQL.Execution;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLEngineConfig
    {
        private readonly List<GraphQLType> _additionalTypes = new List<GraphQLType>();
        private readonly List<GraphQLDirective> _directives = new List<GraphQLDirective>();

        public GraphQLSchemaWrapper Schema { get; set; }
        public IDocumentBuilder DocumentBuilder { get; set; }
        public IEnumerable<GraphQLType> AdditionalTypes => _additionalTypes;
        public IEnumerable<GraphQLDirective> Directives => _directives;

        public void RegisterTypes(params GraphQLType[] types)
        {
            Invariant.Check(types == null, $"{types} cannot be null.");

            types.Apply(RegisterType);
        }

        public void RegisterType<T>() where T : GraphQLType, new()
        {
            RegisterType(new T());
        }

        private void RegisterType(GraphQLType type)
        {
            _additionalTypes.Fill(type);
        }
    }
}
