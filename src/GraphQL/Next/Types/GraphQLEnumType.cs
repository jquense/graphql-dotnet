using GraphQL.Next.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLEnumType : GraphQLScalarType
    {
        private readonly List<EnumValueDefinition> _values = new List<EnumValueDefinition>();

        public GraphQLEnumType(GraphQLEnumTypeConfig config)
            : base(config)
        {
            _values.AddRange(config.Values);
        }

        public IEnumerable<EnumValueDefinition> Values => _values;

        public static GraphQLEnumType For<T>(Action<GraphQLEnumTypeConfig<T>> configure)
        {
            var config = new GraphQLEnumTypeConfig<T>();
            configure(config);
            return new GraphQLEnumType(config);
        }

        public static GraphQLEnumType For<T>(string description = null)
        {
            var type = typeof(T);

            Invariant.Check(type.IsEnum, $"{type.Name} must be of type enum.");

            var config = new GraphQLEnumTypeConfig<T>();
            config.Name = type.Name.PascalCase();
            config.Description = description;

            foreach (var enumName in type.GetEnumNames())
            {
                var enumMember = type
                  .GetMember(enumName, BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                  .First();

                var name = DeriveEnumValueName(enumMember.Name);

                config.Value(name, (T)Enum.Parse(type, enumName));
            }

            return new GraphQLEnumType(config);
        }

        static string DeriveEnumValueName(string name)
        {
            return Regex
              .Replace(name, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4")
              .ToUpperInvariant();
        }
    }


    public class EnumValues : IEnumerable<EnumValueDefinition>
    {
        private readonly List<EnumValueDefinition> _values = new List<EnumValueDefinition>();

        public void Add(EnumValueDefinition value)
        {
            _values.Add(value);
        }

        public IEnumerator<EnumValueDefinition> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class EnumValueDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeprecationReason { get; set; }
        public object Value { get; set; }
    }
}
