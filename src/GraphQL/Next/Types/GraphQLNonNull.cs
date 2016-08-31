using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLNonNull : GraphQLType, IGraphQLInputType, IGraphQLOutputType
    {
        private IGraphQLType _ofType;

        public GraphQLNonNull(IGraphQLType ofType)
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
            Invariant.Check(
                type != null && type.GetType() != typeof(GraphQLNonNull),
                $"Can only create NonNull of a Nullable GraphQLType but got: {type}.");

            _ofType = type;
            Name = $"{type}!";
        }
    }

}
