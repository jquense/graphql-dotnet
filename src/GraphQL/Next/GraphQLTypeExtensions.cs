using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public static class GraphQLExtensions
    {

        public static IGraphQLType GetGraphQLTypeFromType(this Type type)
        {
            // TODO: infer nullable 

            if (type == typeof(int))
            {
                return GraphQLScalarTypes.GraphQLInt;
            }

            if (type == typeof(long))
            {
                return GraphQLScalarTypes.GraphQLInt;
            }

            if (type == typeof(double))
            {
                return GraphQLScalarTypes.GraphQLFloat;
            }

            if (type == typeof(string))
            {
                return GraphQLScalarTypes.GraphQLString;
            }

            if (type == typeof(bool))
            {
                return GraphQLScalarTypes.GraphQLBoolean;
            }


            throw new ArgumentOutOfRangeException(nameof(type), "Unknown input type.");
        }

        public static bool IsCoreType(this IGraphQLType type)
        {
            IGraphQLType[] scalars = {
                GraphQLScalarTypes.GraphQLID,
                GraphQLScalarTypes.GraphQLBoolean,
                GraphQLScalarTypes.GraphQLFloat,
                GraphQLScalarTypes.GraphQLInt,
                GraphQLScalarTypes.GraphQLString
            };

            IGraphQLType[] introspection = {
            };

            return scalars.Any(s => s.Equals(type))
                   || introspection.Any(i => i.Equals(type));
        }

        public static string NameOf<T, P>(this Expression<Func<T, P>> expression)
        {
            var member = (MemberExpression)expression.Body;
            return member.Member.Name;
        }
    }
}
