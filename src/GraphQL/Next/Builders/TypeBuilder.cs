using System;
using GraphQL.Next.Configs;
using GraphQL.Next.Types;
using System.Linq.Expressions;
using GraphQL.Next.Resolvers;
using GraphQL.Next.Builders;

namespace GraphQL.Next.Builders
{
    public class TypeBuilder<TConfigType, TSelf> : IBuilder<TConfigType>
        where TConfigType : IGraphQLTypeConfig
        where TSelf : TypeBuilder<TConfigType, TSelf>
    {
        protected readonly TConfigType _config;

        private GraphQLTypeReference _reference;

        public GraphQLTypeReference Self => _reference;

        public TypeBuilder(TConfigType config)
        {
            _config = config;
        }

        public TSelf Name(string name)
        {
            _config.Name = name;
            _reference = new GraphQLTypeReference(name);

            return (TSelf)this;
        }

        public TSelf Description(string description)
        {
            _config.Description = description;
            return (TSelf)this;
        }

        public TSelf DeprecationReason(string deprecationReason)
        {
            _config.DeprecationReason = deprecationReason;
            return (TSelf)this;
        }

        public virtual TConfigType Resolve()
        {
            return _config;
        }
    }


    public class ComplexTypeBuilder<TSourceType, TConfigType, TSelf> : TypeBuilder<TConfigType, TSelf>
            where TConfigType : GraphQLComplexTypeConfig
            where TSelf : ComplexTypeBuilder<TSourceType, TConfigType, TSelf>
    {
        public ComplexTypeBuilder(TConfigType config) :
            base(config)
        {
        }

        private TSelf Field(
            string name,
            IGraphQLOutputType type,
            string description = null,
            string deprecatedReason = null,
            IFieldResolver resolver = null)
        {
            _config.AddField(new GraphQLFieldDefinition
            {
                Name = name,
                Type = type,
                Description = description,
                DeprecationReason = deprecatedReason,
                Resolver = resolver
            });
            return (TSelf)this;
        }

        /// <summary>
        /// Create a new Field for this GraphQlType
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="type">The GraphQL type of the field</param>
        /// <param name="description"></param>
        /// <param name="deprecatedReason"></param>
        /// <param name="resolve"></param>
        /// <returns></returns>
        public TSelf Field(
            string name,
            IGraphQLOutputType type,
            string description = null,
            string deprecatedReason = null,
            Func<ResolveFieldContext<TSourceType>, object> resolve = null)
        {
            return Field(name, type, description, deprecatedReason, new FuncFieldResolver<TSourceType, object>(resolve));
        }


        public TSelf Field<TProperty>(
            string name,
            Expression<Func<TSourceType, TProperty>> resolve,
            bool nullable = false,
            string description = null,
            string deprecatedReason = null,
            Action<GraphQLArgumentConfig> args = null)
        {
            var argConfig = new GraphQLArgumentConfig();
            var type = typeof(TProperty);
            var outputType = (IGraphQLOutputType)type.GetGraphQLTypeFromType();

            if (!nullable)
            {
                outputType = new GraphQLNonNull(outputType);
            }

            args?.Invoke(argConfig);

            return Field(name, outputType, description, deprecatedReason,
                new ExpressionFieldResolver<TSourceType, TProperty>(resolve));
        }

        public TSelf Field<TProperty>(
            Expression<Func<TSourceType, TProperty>> resolve,
            bool nullable = false,
            string description = null,
            string deprecatedReason = null,
            Action<GraphQLArgumentConfig> args = null)
        {
            return Field(resolve.NameOf(), resolve, nullable, description, deprecatedReason, args);
        }

        public TSelf Field(Action<FieldBuilder<TSourceType>> configure)
        {
            var builder = new FieldBuilder<TSourceType>();
            configure(builder);

            _config.AddField(builder.Resolve());
            return (TSelf)this;
        }
    }

    public class ComplexTypeBuilder<TSourceType, TConfigType> 
        : ComplexTypeBuilder<TSourceType, TConfigType, ComplexTypeBuilder<TSourceType, TConfigType>>
        where TConfigType : GraphQLComplexTypeConfig
    {
        public ComplexTypeBuilder(TConfigType config) :
            base(config)
        {
        }
    }
}
