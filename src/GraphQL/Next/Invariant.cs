using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public class Invariant
    {
        public static void Check(bool valid, string message)
        {
            if (!valid)
            {
                throw new GraphQLError(message);
            }
        }
    }
}
