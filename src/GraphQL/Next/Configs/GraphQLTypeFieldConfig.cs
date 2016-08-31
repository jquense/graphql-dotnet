using GraphQL.Next.Resolvers;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLTypeFieldConfig : IGraphQLComplexTypeConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private readonly List<GraphQLFieldDefinition> _fields = new List<GraphQLFieldDefinition>();
        public IEnumerable<GraphQLFieldDefinition> Fields => _fields;

        public void Field(GraphQLFieldDefinition field)
        {
            _fields.Add(field);
        }

        public void Field(Action<GraphQLFieldConfig> fieldConfig)
        {
            var config = new GraphQLFieldConfig();
            fieldConfig(config);

            var def = new GraphQLFieldDefinition
            {
                Name = config.Name,
                Type = config.Type,
                Description = config.Description,
                DeprecationReason = config.DeprecationReason,
                Resolver = config.Resolve,
                Arguments = config.Arguments
            };

            _fields.Add(def);
        }

        public void Field<TReturnType>(
            string name,
            IGraphQLOutputType type,
            string description = null,
            string deprecationReason = null,
            Func<ResolveFieldContext<object>, TReturnType> resolve = null,
            Action<GraphQLArgumentConfig> args = null)
        {
            var def = new GraphQLFieldDefinition
            {
                Name = name,
                Description = description,
                DeprecationReason = deprecationReason,
                Type = type,
                Resolver = new FuncFieldResolver<object, TReturnType>(resolve)
            };

            if (args != null)
            {
                var argConfig = new GraphQLArgumentConfig();
                args(argConfig);
                def.Arguments = argConfig.Arguments;
            }

            _fields.Add(def);
        }

        public void Field(
            string name,
            IGraphQLOutputType type,
            string description = null,
            string deprecationReason = null,
            Func<ResolveFieldContext<object>, object> resolve = null,
            Action<GraphQLArgumentConfig> args = null)
        {
            Field<object>(name, type, description, deprecationReason, resolve, args);
        }
    }

    public class GraphQLTypeFieldConfig<TSourceType> : GraphQLTypeFieldConfig
    {
        public void Field<TProperty>(
            Expression<Func<TSourceType, TProperty>> resolve,
            bool nullable = false,
            string description = null,
            string deprecatedReason = null,
            Action<GraphQLArgumentConfig> args = null)
        {
            var name = resolve.NameOf();
            Field(name, resolve, nullable, description, deprecatedReason, args);
        }

        public void Field<TProperty>(
            string name,
            Expression<Func<TSourceType, TProperty>> resolve,
            bool nullable = false,
            string description = null,
            string deprecatedReason = null,
            Action<GraphQLArgumentConfig> args = null)
        {
            var def = new GraphQLFieldDefinition();
            def.Name = name.CamelCase();

            var type = typeof(TProperty);
            var outputType = (IGraphQLOutputType)type.GetGraphQLTypeFromType();

            if (!nullable)
            {
                outputType = new GraphQLNonNull(outputType);
            }

            def.Type = outputType;
            def.Resolver = new ExpressionFieldResolver<TSourceType, TProperty>(resolve);

            if (args != null)
            {
                var argConfig = new GraphQLArgumentConfig();
                args(argConfig);
                def.Arguments = argConfig.Arguments;
            }

            Field(def);
        }

        public void Field<TProperty>(
            string name,
            IGraphQLOutputType type,
            Expression<Func<TSourceType, TProperty>> resolve,
            string description = null,
            string deprecatedReason = null)
        {
            var def = new GraphQLFieldDefinition
            {
                Name = name,
                Type = type,
                Description = description,
                DeprecationReason = deprecatedReason,
                Resolver = new ExpressionFieldResolver<TSourceType, TProperty>(resolve)
            };

            Field(def);
        }

        public void Field(
            string name,
            IGraphQLOutputType type,
            string description = null,
            string deprecatedReason = null,
            Func<ResolveFieldContext<TSourceType>, object> resolve = null)
        {
            var def = new GraphQLFieldDefinition
            {
                Name = name,
                Type = type,
                Description = description,
                DeprecationReason = deprecatedReason,
                Resolver = new FuncFieldResolver<TSourceType, object>(resolve)
            };

            Field(def);
        }
    }
}
