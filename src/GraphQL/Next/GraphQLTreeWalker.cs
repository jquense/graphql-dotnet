using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Next.Types;

namespace GraphQL.Next
{
    public class GraphQLTreeWalker
    {
        private readonly List<ReferenceTarget> _references = new List<ReferenceTarget>();

        public Dictionary<string, IGraphQLType> Walk(GraphQLSchemaWrapper schema)
        {
            var types = new Dictionary<string, IGraphQLType>(StringComparer.OrdinalIgnoreCase);

            AddType(types, schema.Query);
            AddType(types, schema.Mutation);

            // replace any GraphQLReferenceTypes with real types
            _references.Apply(x =>
            {
                var refType = types[x.Ref.TypeName];
                x.Replacer.Replace(x.Target, refType);
            });

            return types;
        }

        private void AddType(Dictionary<string, IGraphQLType> types, IGraphQLType type)
        {
            if (type == null)
            {
                return;
            }

            if (type is GraphQLNonNull)
            {
                var nonNull = (GraphQLNonNull)type;
                if (nonNull.OfType is GraphQLTypeReference)
                {
                    var re = new ReferenceTarget
                    {
                        Ref = (GraphQLTypeReference)nonNull.OfType,
                        Target = nonNull,
                        Replacer =
                            new ReferenceReplacer<GraphQLNonNull, IGraphQLType>(
                                (target, repl) =>
                                {
                                    target.OfType = repl;
                                })
                    };

                    _references.Add(re);
                    return;
                }

                AddType(types, nonNull.OfType);
                return;
            }

            if (type is GraphQLList)
            {
                var list = (GraphQLList)type;
                if (list.OfType is GraphQLTypeReference)
                {
                    var re = new ReferenceTarget
                    {
                        Ref = (GraphQLTypeReference)list.OfType,
                        Target = list,
                        Replacer =
                            new ReferenceReplacer<GraphQLList, IGraphQLType>(
                                (target, repl) =>
                                {
                                    target.OfType = repl;
                                })
                    };

                    _references.Add(re);
                    return;
                }

                AddType(types, list.OfType);
                return;
            }

            if (type is GraphQLTypeReference)
            {
                return;
            }

            if (type.IsCoreType())
            {
                return;
            }

            if (types.ContainsKey(type.Name))
            {
                return;
            }

            types[type.Name] = type;

            if (type is GraphQLObjectType)
            {
                var objType = (GraphQLObjectType)type;
                objType.Fields.Apply(x =>
                {
                    x.Arguments?.Apply(a => AddType(types, a.Type));
                    AddType(types, x.Type);
                });

                objType.Interfaces.Apply(x =>
                {
                    x.AddTypes(objType);
                    AddType(types, x);
                });
            }

            if (type is GraphQLInterfaceType)
            {
                var inter = (GraphQLInterfaceType)type;
                inter.Fields.Apply(x =>
                {
                    x.Arguments?.Apply(a => AddType(types, a.Type));
                    if (x.Type is GraphQLTypeReference)
                    {
                        var re = new ReferenceTarget
                        {
                            Ref = (GraphQLTypeReference)x.Type,
                            Target = x,
                            Replacer =
                                new ReferenceReplacer<GraphQLFieldDefinition, IGraphQLOutputType>(
                                    (target, repl) =>
                                    {
                                        target.Type = repl;
                                    })
                        };

                        _references.Add(re);
                    }
                    else
                    {
                        AddType(types, x.Type);
                    }
                });
            }

            if (type is GraphQLUnionType)
            {
                var union = (GraphQLUnionType)type;
                union.Types.Apply(x =>
                {
                    AddType(types, x);
                });
            }
        }
    }
}
