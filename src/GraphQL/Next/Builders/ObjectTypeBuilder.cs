using System;
using GraphQL.Next.Configs;
using GraphQL.Next.Types;
using System.Linq;
using System.Linq.Expressions;
using GraphQL.Next.Resolvers;
using GraphQL.Next.Builders;

namespace GraphQL.Next.Builders
{
    
    public class ObjectTypeBuilder<TSourceType> : ComplexTypeBuilder<TSourceType, GraphQLObjectTypeConfig, ObjectTypeBuilder<TSourceType>>
    {
        public ObjectTypeBuilder(GraphQLObjectTypeConfig config) : base(config) {
            _config.IsOfType = type => type is TSourceType;
        }

        public ObjectTypeBuilder<TSourceType> IsOfType(Func<object, bool> checker)
        {
            _config.IsOfType = checker;
            return this;
        }

        public ObjectTypeBuilder<TSourceType> Interface(params GraphQLInterfaceType[] interfaces)
        {
            _config.Interface(interfaces);
            return this;
        }
    }
}
