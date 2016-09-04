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

        public GraphQLUnionType(GraphQLUnionTypeConfig config) : base(config)
        {
        }

        public static GraphQLUnionType For(Action<GraphQLUnionTypeConfig> configure)
        {
            var config = new GraphQLUnionTypeConfig();
            configure(config);
            return new GraphQLUnionType(config);
        }

        public override void Initialize()
        {
            base.Initialize();

            Invariant.Check(
                Types != null && Types.Any(),
                $"{Name} has no possible types configured.");
        }
    }
}
