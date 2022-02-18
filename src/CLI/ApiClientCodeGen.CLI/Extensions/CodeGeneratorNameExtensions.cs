using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Commands;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Extensions
{
    public static class CodeGeneratorNameExtensions
    {
        public static string GetCodeGeneratorName(this CodeGeneratorCommand generator)
        {
            var type = generator.GetType();
            
            if (type == typeof(OpenApiGeneratorCommand))
                return SupportedCodeGenerator.OpenApi.GetName();
            
            if (type == typeof(SwaggerCodegenCommand))
                return SupportedCodeGenerator.Swagger.GetName();
            
            return type.Name.Replace("Command", string.Empty);
        }
    }
}