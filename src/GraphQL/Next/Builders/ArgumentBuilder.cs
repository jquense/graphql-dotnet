using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Builders
{
    public class ArgumentBuilder<TInputType, TType> : IBuilder<GraphQLArgument>
        where TInputType : IGraphQLInputType
    {
        private readonly GraphQLArgument _argument ;

        public ArgumentBuilder()
        {
            _argument = new GraphQLArgument();

            var type = typeof(TInputType);

            DefaultValue(default(TType));
        }

        public ArgumentBuilder(GraphQLArgument argument)
        {
            _argument = argument;
        }

        public ArgumentBuilder<TInputType, TNewType> Returns<TNewType>()
        {
            return new ArgumentBuilder<TInputType, TNewType>(_argument);
        }

        public ArgumentBuilder<TInputType, TType> Name(string name)
        {
            _argument.Name = name;
            return this;
        }

        public ArgumentBuilder<TInputType, TType> Description(string description)
        {
            _argument.Description = description;
            return this;
        }

        public ArgumentBuilder<TInputType, TType> DeprecationReason(string deprecationReason)
        {
            _argument.DeprecationReason = deprecationReason;
            return this;
        }

        public ArgumentBuilder<TInputType, TType> DefaultValue(object defaultValue)
        {
            _argument.DefaultValue = defaultValue;
            return this;
        }

        public GraphQLArgument Resolve()
        {
            return _argument;
        }
    }
}
