using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLScalarTypeConfig : GraphQLTypeConfig
    {
        public Func<object, object> Serialize { get; set; }
        public Func<object, object> ParseValue { get; set; }
        public Func<IValue, object> ParseLiteral { get; set; }
    }

    public class GraphQLScalarTypeConfig<T> : GraphQLScalarTypeConfig
    {
        public new Func<object, T> ParseValue { get; set; }
        public new Func<IValue, T> ParseLiteral { get; set; }
    }
}
