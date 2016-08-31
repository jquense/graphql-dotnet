using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public static class GraphQLScalarTypes
    {
        public static GraphQLScalarType GraphQLInt = GraphQLScalarType.For(_ =>
        {
            _.Name = "Int";
        });

        public static GraphQLScalarType<double> GraphQLFloat = GraphQLScalarType<double>.For(_ =>
        {
            _.Name = "Float";
        });

        public static GraphQLScalarType<string> GraphQLString = GraphQLScalarType<string>.For(_ =>
        {
            _.Name = "String";
        });

        public static GraphQLScalarType<bool> GraphQLBoolean = GraphQLScalarType<bool>.For(_ =>
        {
            _.Name = "Boolean";
        });

        public static GraphQLScalarType GraphQLID = GraphQLScalarType.For(_ =>
        {
            _.Name = "ID";
        });
    }
}
