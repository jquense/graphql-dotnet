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
        private readonly List<IGraphQLType> _types = new List<IGraphQLType>();

        internal GraphQLAbstractType()
        {
        }

        public string Description { get; set; }

        public IEnumerable<IGraphQLType> Types => _types;

        protected void Initialize(IGraphQLAbstractTypeConfig config)
        {
            Name = config.Name;
            Description = config.Description;
        }

        public bool IsPossibleType(IGraphQLType type)
        {
            return Types.Any(x => x.Equals(type));
        }

        public void AddTypes(params IGraphQLType[] types)
        {
            _types.Fill(types);
        }
    }
}
