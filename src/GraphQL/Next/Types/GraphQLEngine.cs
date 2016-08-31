using GraphQL.Execution;
using GraphQL.Language.AST;
using GraphQL.Next.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Next.Types
{
    public class GraphQLEngine
    {
        private readonly GraphQLEngineConfig _config;

        public GraphQLEngine(GraphQLEngineConfig config)
        {
            _config = config;
        }

        public async Task<ExecutionResult> ExecuteAsync(
            string query,
            string operationName = null,
            object root = null,
            Inputs inputs = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new ExecutionResult();
            var document = _config.DocumentBuilder.Build(query);

            var context = BuildExecutionContext(_config.Schema, root, document, operationName, inputs, cancellationToken);

            if (context.Errors.Any())
            {
                result.Errors = context.Errors;
                return result;
            }

            //            result.Data = await ExecuteOperation(context);
            if (context.Errors.Any())
            {
                result.Errors = context.Errors;
            }

            return result;
        }

        public static GraphQLEngine For(object schema)
        {
            var engineConfig = new GraphQLEngineConfig();
            engineConfig.DocumentBuilder = new GraphQLDocumentBuilder();
            engineConfig.Schema = new GraphQLSchemaWrapper(schema);
            return new GraphQLEngine(engineConfig);
        }

        public GraphQLExecutionContext BuildExecutionContext(
            GraphQLSchemaWrapper schema,
            object root,
            Document document,
            string operationName,
            Inputs inputs,
            CancellationToken cancellationToken)
        {
            var context = new GraphQLExecutionContext();
            context.Schema = schema;
            context.RootValue = root;

            var operation = !string.IsNullOrWhiteSpace(operationName)
                ? document.Operations.WithName(operationName)
                : document.Operations.FirstOrDefault();

            if (operation == null)
            {
                context.Errors.Add(new ExecutionError("Unknown operation name: {0}".ToFormat(operationName)));
                return context;
            }

            context.Operation = operation;
            //            context.Variables = GetVariableValues(schema, operation.Variables, inputs);
            context.Fragments = document.Fragments;
            context.CancellationToken = cancellationToken;

            return context;
        }
    }
}
