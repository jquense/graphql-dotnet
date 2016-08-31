using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLArgumentConfig
    {
        private readonly List<GraphQLArgument> _arguments = new List<GraphQLArgument>();

        public IEnumerable<GraphQLArgument> Arguments => _arguments;

        public void Argument(GraphQLArgument argument)
        {
            _arguments.Add(argument);
        }

        public void Argument(Action<GraphQLArgument> configure)
        {
            var arg = new GraphQLArgument();
            configure(arg);
            _arguments.Add(arg);
        }

        public void Argument<T>(
            string name,
            string description = null,
            string deprecationReason = null,
            T defaultValue = default(T),
            bool nullable = false)
        {
            var type = typeof(T);
            var inputType = (IGraphQLInputType)type.GetGraphQLTypeFromType();

            if (!nullable)
            {
                inputType = new GraphQLNonNull(inputType);
            }

            Argument(name, inputType, description, deprecationReason, defaultValue);
        }

        public void Argument(
            string name,
            IGraphQLInputType type,
            string description = null,
            string deprecationReason = null,
            object defaultValue = null)
        {
            var arg = new GraphQLArgument
            {
                Name = name,
                Type = type,
                Description = description,
                DeprecationReason = deprecationReason,
                DefaultValue = defaultValue,
            };
            _arguments.Add(arg);
        }
    }
}
