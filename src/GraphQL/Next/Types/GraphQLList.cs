using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLList : GraphQLType, IGraphQLInputType, IGraphQLOutputType
    {
        private IGraphQLType _ofType;

        public GraphQLList(IGraphQLType ofType)
        {
            SetValue(ofType);
        }

        public IGraphQLType OfType
        {
            get { return _ofType; }
            set
            {
                SetValue(value);
            }
        }

        private void SetValue(IGraphQLType type)
        {
            Invariant.Check(type != null, $"Can only create a list of GraphQLType but got: {type}");

            _ofType = type;
            Name = $"[{type}]";
        }
    }

}
