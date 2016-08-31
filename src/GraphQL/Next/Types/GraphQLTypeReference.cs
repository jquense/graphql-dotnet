﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    /// <summary>
    /// A special type to allow an object/interface to reference itself.
    /// It is replaced with the real type object when the schema is built.
    /// </summary>
    public class GraphQLTypeReference : GraphQLType, IGraphQLOutputType
    {
        public GraphQLTypeReference(string typeName)
        {
            Name = $"ref({typeName})";
            TypeName = typeName;
        }

        public string TypeName { get; set; }
    }
}