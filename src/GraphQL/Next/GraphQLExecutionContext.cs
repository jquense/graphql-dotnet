using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public class GraphQLExecutionContext
    {
        public GraphQLExecutionContext()
        {
            Fragments = new Fragments();
            Errors = new ExecutionErrors();
        }

        public GraphQLSchemaWrapper Schema { get; set; }

        public object RootValue { get; set; }

        public Operation Operation { get; set; }

        public Fragments Fragments { get; set; }

        public Variables Variables { get; set; }

        public ExecutionErrors Errors { get; set; }

        public CancellationToken CancellationToken { get; set; }
    }
}
