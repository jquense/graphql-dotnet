using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLInputObjectTypeConfig
    {
        private readonly List<GraphQLInputObjectField> _fields = new List<GraphQLInputObjectField>();

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<GraphQLInputObjectField> Fields => _fields;

        public void Field(GraphQLInputObjectField field)
        {
            _fields.Add(field);
        }

        public void Field(Action<GraphQLInputObjectField> configure)
        {
            var field = new GraphQLInputObjectField();
            configure(field);
            _fields.Add(field);
        }

        public void Field(
            string name,
            IGraphQLInputType type,
            string description = null,
            object defaultValue = null)
        {
            _fields.Add(new GraphQLInputObjectField
            {
                Name = name,
                Type = type,
                Description = description,
                DefaultValue = defaultValue
            });
        }

        public void Field<TProperty>(
            string name,
            IGraphQLInputType type,
            string description = null,
            TProperty defaultValue = default(TProperty))
        {
            _fields.Add(new GraphQLInputObjectField
            {
                Name = name,
                Type = type,
                Description = description,
                DefaultValue = defaultValue
            });
        }

        public void Field<TProperty>(
            string name,
            string description = null,
            TProperty defaultValue = default(TProperty))
        {
            var type = (IGraphQLInputType)typeof(TProperty).GetGraphQLTypeFromType();
            Field(name, type, description, defaultValue);
        }
    }

    public class GraphQLInputObjectTypeConfig<T> : GraphQLInputObjectTypeConfig
    {
        public void Field<TProperty>(
            Expression<Func<T, TProperty>> name,
            string description = null,
            TProperty defaultValue = default(TProperty))
        {
            var fieldName = name.NameOf();
            Field(fieldName, description, defaultValue);
        }
    }
}
