using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLAbstractType : GraphQLType, IGraphQLOutputType
    {
        // TODO: needs to be public?
        public IList<IGraphQLType> Types => ((IGraphQLAbstractTypeConfig) TypeConfig).Types;

        internal GraphQLAbstractType(IGraphQLAbstractTypeConfig config) : base(config){}

        public bool IsPossibleType(IGraphQLType type)
        {
            return Types.Any(x => x.Equals(type));
        }

        public void AddTypes(params IGraphQLType[] types)
        {
            TypeConfig.As<IGraphQLAbstractTypeConfig>().Types.Fill(types);
        }
    }
}
