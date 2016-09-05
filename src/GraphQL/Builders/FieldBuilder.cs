using System;
using GraphQL.Types;
using GraphQL.Resolvers;
using System.Linq.Expressions;

namespace GraphQL.Builders
{
    public static class FieldBuilder
    {
        public static FieldBuilder<TSourceType, TReturnType> Create<TSourceType, TReturnType>(Type type = null)
        {
            return FieldBuilder<TSourceType, TReturnType>.Create(type);
        }
    }

    public class FieldBuilder<TSourceType, TReturnType>
    {

        private FieldType _fieldType { get; set; }

        public FieldType FieldType => _fieldType;

        private FieldBuilder(FieldType fieldType)
        {
            _fieldType = fieldType;
        }

        public static FieldBuilder<TSourceType, TReturnType> Create(Type type = null)
        {
            var fieldType = new FieldType
            {
                Type = type,
                Arguments = new QueryArguments(),
            };
            return new FieldBuilder<TSourceType, TReturnType>(fieldType);
        }

        public FieldBuilder<TSourceType, TReturnType> Name(string name)
        {
            _fieldType.Name = name;
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> Description(string description)
        {
            _fieldType.Description = description;
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> DeprecationReason(string deprecationReason)
        {
            _fieldType.DeprecationReason = deprecationReason;
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> DefaultValue(TReturnType defaultValue = default(TReturnType))
        {
            _fieldType.DefaultValue = defaultValue;
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> Type(Type type)
        {
            _fieldType.Type = type;
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> Type<TGraphType>() where TGraphType : IGraphType
        {
            return Type(typeof (TGraphType));
        }

        public FieldBuilder<TSourceType, TReturnType> For(Func<ResolveFieldContext<TSourceType>, TReturnType> resolve)
        {
            _fieldType.Type = _fieldType.Type ?? typeof(TReturnType).GetGraphTypeFromType();
            _fieldType.Resolver = new FuncFieldResolver<TSourceType, TReturnType>(resolve);

            return this;
        }

        public FieldBuilder<TSourceType, TNewType> For<TNewType>(Func<ResolveFieldContext<TSourceType>, TNewType> resolve)
        {
            return new FieldBuilder<TSourceType, TNewType>(_fieldType).For(resolve);
        }




        public FieldBuilder<TSourceType, TReturnType> For(Expression<Func<TSourceType, TReturnType>> resolve)
        {
            _fieldType.Type = _fieldType.Type ?? typeof(TReturnType).GetGraphTypeFromType();
            _fieldType.Name = resolve.NameOf().ToCamelCase();
            _fieldType.Resolver = new ExpressionFieldResolver<TSourceType, TReturnType>(resolve);

            return this;
        }

        public FieldBuilder<TSourceType, TNewType> For<TNewType>(Expression<Func<TSourceType, TNewType>> resolve)
        {
            return new FieldBuilder<TSourceType, TNewType>(_fieldType).For(resolve);
        }


        public FieldBuilder<TSourceType, TReturnType> Resolve(IFieldResolver resolver)
        {
            _fieldType.Resolver = resolver;
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> Resolve(Func<ResolveFieldContext<TSourceType>, TReturnType> resolve)
        {
            return Resolve(new FuncFieldResolver<TSourceType, TReturnType>(resolve));
        }

        public FieldBuilder<TSourceType, TNewReturnType> Returns<TNewReturnType>()
        {
            return new FieldBuilder<TSourceType, TNewReturnType>(FieldType);
        }

        public FieldBuilder<TSourceType, TReturnType> Argument<TArgumentGraphType>(string name, string description)
        {
            _fieldType.Arguments.Add(new QueryArgument(typeof(TArgumentGraphType))
            {
                Name = name,
                Description = description,
            });
            return this;
        }

        public FieldBuilder<TSourceType, TReturnType> Argument<TArgumentGraphType, TArgumentType>(string name, string description,
            TArgumentType defaultValue = default(TArgumentType))
        {
            _fieldType.Arguments.Add(new QueryArgument(typeof(TArgumentGraphType))
            {
                Name = name,
                Description = description,
                DefaultValue = defaultValue,
            });
            return this;
        }
    }
}
