using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public class GraphQLError : Exception
    {
        public GraphQLError(string message)
            : base(message)
        {
        }
    }
}
