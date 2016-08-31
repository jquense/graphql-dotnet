using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLUnionType : GraphQLAbstractType
    {
        public GraphQLUnionType()
        {
        }

        public GraphQLUnionType(GraphQLUnionTypeConfig config)
        {
            Initialize(config);
        }

        public void Initialize(GraphQLUnionTypeConfig config)
        {
            base.Initialize(config);
            AddTypes(config.Types);
        }

        public static GraphQLUnionType For(Action<GraphQLUnionTypeConfig> configure)
        {
            var config = new GraphQLUnionTypeConfig();
            configure(config);
            return new GraphQLUnionType(config);
        }
    }
}
