using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.OpenApiGenerator;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.OpenApiGenerator
{
    [ExcludeFromCodeCoverage]
    public class OpenApiGeneratorOptionsPage : DialogPage, IOpenApiGeneratorOptions
    {
        public const string Name = "OpenAPI Generator";
        
        [Category(Name)]
        [DisplayName("Emit Default Value")]
        [Description("Set to true if the default value for a member should be generated in the serialization stream")]
        public bool EmitDefaultValue { get; set; }
    }
}