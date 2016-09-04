using GraphQL.Next.Builders;
using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public interface IGraphQLObjectTypeConfig : IGraphQLComplexTypeConfig
    {
        Func<object, bool> IsOfType { get; set; }
        IEnumerable<GraphQLInterfaceType> Interfaces { get; }
        void Interface(params GraphQLInterfaceType[] interfaces);
    }


    public class GraphQLObjectType : GraphQLType, IGraphQLOutputType
    {
        public Func<object, bool> IsOfType => TypeConfig.As<IGraphQLObjectTypeConfig>().IsOfType;

        private readonly List<GraphQLInterfaceType> _interfaces = new List<GraphQLInterfaceType>();

        public IEnumerable<GraphQLInterfaceType> Interfaces
        {
            get { return _interfaces; }
            set
            {
                _interfaces.Clear();
                _interfaces.AddRange(value);
            }
        }


        internal void Initialize(GraphQLObjectTypeConfig config)
        {
            base.Initialize(config);

            if (Interfaces != null && Interfaces.Any())
            {
                Invariant.Check(
                    IsOfType != null,
                    $"{Name} does not provide a \"isTypeOf\" function.  There is no way to resolve this "
                    + "implementing type during execution.");
            }
        }

        public ObjectTypeBuilder<TSourceType> For<TSourceType>()
        {
            Initialize(new GraphQLObjectTypeConfig());

            return new ObjectTypeBuilder<TSourceType>(TypeConfig as GraphQLObjectTypeConfig);
        }


        public static GraphQLObjectType For<TSourceType>(Action<ObjectTypeBuilder<TSourceType>> configure)
        {
            var instance = new GraphQLObjectType();

            configure(instance.For<TSourceType>());

            return instance;
        }
    }
}
