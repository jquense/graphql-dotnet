using System;
using GraphQL.Next.Configs;
using GraphQL.Next.Types;
using System.Linq.Expressions;
using GraphQL.Next.Resolvers;
using GraphQL.Next.Builders;

namespace GraphQL.Next.Builders
{

    public class EnumBuilder : TypeBuilder<GraphQLEnumTypeConfig, EnumBuilder>
    {
        public EnumBuilder(GraphQLEnumTypeConfig config) : base(config)
        {
        }

        public EnumBuilder Value(string name, object value, string description = null, string deprecationReason = null)
        {
            _config.AddValue(new EnumValueDefinition
            {
                Name = name,
                Value = value,
                Description = description,
                DeprecationReason = deprecationReason
            });
            return this;
        }
    }
}