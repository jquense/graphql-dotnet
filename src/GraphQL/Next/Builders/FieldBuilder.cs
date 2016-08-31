using System;
using GraphQL.Next.Configs;
using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;

namespace GraphQL.Next.Builders
{

    public class FieldBuilder<TSourceType> : IBuilder<GraphQLFieldDefinition>
    {
        private ArgumentsBuilder _arguments = new ArgumentsBuilder();

        private readonly GraphQLFieldDefinition _field = new GraphQLFieldDefinition();

        public FieldBuilder<TSourceType> Type(IGraphQLOutputType type)
        {
            _field.Type = type;
            return this;
        }

        public FieldBuilder<TSourceType> Name(string name)
        {
            _field.Name = name;
            return this;
        }

        public FieldBuilder<TSourceType> Description(string description)
        {
            _field.Description = description;
            return this;
        }

        public FieldBuilder<TSourceType> DeprecationReason(string deprecationReason)
        {
            _field.DeprecationReason = deprecationReason;
            return this;
        }

        public FieldBuilder<TSourceType> Argument<TInputType, TType>(Action<ArgumentBuilder<TInputType, TType>> configure)
            where TInputType : IGraphQLInputType
        {
            _arguments.Argument<TInputType, TType>(configure);
            return this;
        }

        public FieldBuilder<TSourceType> Argument<TInputType>(Action<ArgumentBuilder<TInputType, object>> configure)
            where TInputType : IGraphQLInputType
        {
            _arguments.Argument<TInputType>(configure);
            return this;
        }

        public FieldBuilder<TSourceType> Resolve(Func<ResolveFieldContext<TSourceType>, object> resolver)
        {
            _field.Resolver = new FuncFieldResolver<TSourceType, object>(resolver);
            return this;
        }

        public FieldBuilder<TSourceType> Resolve<TReturnType>(Func<ResolveFieldContext<TSourceType>, TReturnType> resolver)
        {
            _field.Resolver = new FuncFieldResolver<TSourceType, TReturnType>(resolver);
            return this;
        }

        public GraphQLFieldDefinition Resolve()
        {
            _field.Arguments = _arguments.Resolve();

            return _field;
        }
    }
}
