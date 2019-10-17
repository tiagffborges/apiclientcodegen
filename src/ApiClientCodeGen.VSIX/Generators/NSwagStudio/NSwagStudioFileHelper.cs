﻿using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.NSwagStudio;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio
{
    public static class NSwagStudioFileHelper
    {
        public static async Task<string> CreateNSwagStudioFileAsync(
            string openApiSpec, 
            string openApiSpecUrl,
            INSwagStudioOptions options = null,
            string outputNamespace = null)
        {
            var openApiDocument = await OpenApiDocument.FromJsonAsync(openApiSpec);
            var className = openApiDocument.GenerateClassName(options?.UseDocumentTitle ?? true);

            return new
                {
                    Runtime = "Default",
                    SwaggerGenerator = new
                    {
                        FromSwagger = new
                        {
                            Json = openApiSpec,
                            Url = openApiSpecUrl
                        }
                    },
                    CodeGenerators = new
                    {
                        SwaggerToCSharpClient = new
                        {
                            ClassName = className,
                            InjectHttpClient = options?.InjectHttpClient ?? true,
                            GenerateClientInterfaces = options?.GenerateClientInterfaces ?? true,
                            GenerateDtoTypes = options?.GenerateDtoTypes ?? true,
                            UseBaseUrl = options?.UseBaseUrl ?? false,
                            OperationGenerationMode = "MultipleClientsFromOperationId",
                            GenerateResponseClasses = options?.GenerateResponseClasses ?? true,
                            GenerateJsonMethods = options?.GenerateJsonMethods ?? true,
                            RequiredPropertiesMustBeDefined = options?.RequiredPropertiesMustBeDefined ?? true,
                            ClassStyle = options?.ClassStyle ?? CSharpClassStyle.Poco,
                            GenerateDefaultValues = options?.GenerateDefaultValues ?? true,
                            GenerateDataAnnotations = options?.GenerateDataAnnotations ?? true,
                            Namespace = outputNamespace ?? "GeneratedCode",
                            Output = $"{className}.cs"
                        }
                    }
                }
                .ToJson();
        }
    }
}
