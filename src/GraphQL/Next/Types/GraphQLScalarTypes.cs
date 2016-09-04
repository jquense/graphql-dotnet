using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLString : GraphQLScalarType<string>
    {
        public GraphQLString() : base ("String") {}
    }

    public class GraphQLInt : GraphQLScalarType<int>
    {
        public GraphQLInt() : base("Int") { }
    }

    public class GraphQLFloat : GraphQLScalarType<double>
    {
        public GraphQLFloat() : base("Float") { }
    }

    public class GraphQLBoolean : GraphQLScalarType<bool>
    {
        public GraphQLBoolean() : base("Boolean") { }
    }

    public class GraphQLID : GraphQLScalarType<string>
    {
        public GraphQLID() : base("ID") { }
    }

    public static class GraphQLScalarTypes
    {
        public static GraphQLInt GraphQLInt = new GraphQLInt();

        public static GraphQLFloat GraphQLFloat = new GraphQLFloat();

        public static GraphQLString GraphQLString = new GraphQLString();

        public static GraphQLBoolean GraphQLBoolean = new GraphQLBoolean();

        public static GraphQLID GraphQLID = new GraphQLID();
    }
}
