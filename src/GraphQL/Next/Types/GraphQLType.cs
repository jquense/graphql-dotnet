using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public interface IGraphQLType
    {
        string Name { get; set; }
    }

    public interface IGraphQLOutputType : IGraphQLType
    {
    }

    public interface IGraphQLInputType : IGraphQLType
    {
    }

    public abstract class GraphQLType : IGraphQLType
    {
        internal GraphQLType() {}

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
