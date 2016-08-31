using GraphQL.Next.Builders;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Builders
{
    public class ArgumentsBuilder : IBuilder<IEnumerable<GraphQLArgument>>
    {
        private readonly List<GraphQLArgument> _arguments = new List<GraphQLArgument>();

        public ArgumentsBuilder Argument<TInputType, TType>(Action<ArgumentBuilder<TInputType, TType>> configure) 
            where TInputType : IGraphQLInputType
        {
            var builder = new ArgumentBuilder<TInputType, TType>();
            configure(builder);
            _arguments.Add(builder.Resolve());

            return this;
        }

        public ArgumentsBuilder Argument<TInputType>(Action<ArgumentBuilder<TInputType, object>> configure)
            where TInputType : IGraphQLInputType
        {
            return Argument<TInputType, object>(configure);
        }

        public ArgumentsBuilder Argument<TInputType, TType>(
            string name,
            string description = null,
            string deprecationReason = null,
            TType defaultValue = default(TType))
            where TInputType : IGraphQLInputType
        {
            return Argument<TInputType, TType>(_ => _
                .Name(name)
                .Description(description)
                .DefaultValue(defaultValue)
            );
        }

        public ArgumentsBuilder Argument<TInputType>(
            string name,
            string description = null,
            string deprecationReason = null)
            where TInputType : IGraphQLInputType
        {
            return Argument<TInputType, object>(name, description = null, deprecationReason = null);
        }

        public IEnumerable<GraphQLArgument> Resolve()
        {
            return _arguments;
        }
    }
}
