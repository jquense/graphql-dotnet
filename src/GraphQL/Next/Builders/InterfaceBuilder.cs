using System;
using GraphQL.Next.Configs;
using GraphQL.Next.Types;
using System.Linq.Expressions;
using GraphQL.Next.Resolvers;
using GraphQL.Next.Builders;

namespace GraphQL.Next.Builders
{

    public class InterfaceBuilder<TSourceType> 
        : ComplexTypeBuilder<TSourceType, GraphQLInterfaceTypeConfig, InterfaceBuilder<TSourceType>>
    {
        public InterfaceBuilder(GraphQLInterfaceTypeConfig config) : base(config)
        {
        }
    }
}