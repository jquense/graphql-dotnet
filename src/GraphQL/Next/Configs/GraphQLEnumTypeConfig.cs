using GraphQL.Language.AST;
using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Configs
{
    public class GraphQLEnumTypeConfig : GraphQLScalarTypeConfig
    {
        private readonly List<EnumValueDefinition> _values = new List<EnumValueDefinition>();

        public GraphQLEnumTypeConfig()
        {
            Serialize = value =>
            {
                var found = _values.FirstOrDefault(v => v.Value.Equals(value));
                return found?.Name;
            };

            ParseValue = value =>
            {
                var found = _values.FirstOrDefault(v =>
                    StringComparer.InvariantCultureIgnoreCase.Equals(v.Name, value.ToString()));
                return found?.Value;
            };

            ParseLiteral = value =>
            {
                var enumVal = value as EnumValue;
                return ParseValue(enumVal?.Name);
            };
        }

        public IEnumerable<EnumValueDefinition> Values => _values;

        protected void AddValue(EnumValueDefinition definition)
        {
            _values.Add(definition);
        }
    }

    public class GraphQLEnumTypeConfig<T> : GraphQLEnumTypeConfig
    {
        public void Value(string name, T value, string description = null, string deprecationReason = null)
        {
            var def = new EnumValueDefinition
            {
                Name = name,
                Value = value,
                Description = description,
                DeprecationReason = deprecationReason
            };
            AddValue(def);
        }
    }
}
