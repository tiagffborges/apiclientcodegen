﻿namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class OpenApiGeneratorProjectFileContents
    {
        public const string NetCoreApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net60</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""JsonSubTypes"" Version=""2.0.1"" />
    <PackageReference Include = ""RestSharp"" Version=""110.2.0"" />
    <PackageReference Include = ""Polly"" Version=""7.2.4"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";

        public const string NetStandardLibrary =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""JsonSubTypes"" Version=""2.0.1"" />
    <PackageReference Include = ""RestSharp"" Version=""110.2.0"" />
    <PackageReference Include = ""Polly"" Version=""7.2.4"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";

        public const string NetFrameworkApp =
            @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net481</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""System"" />
    <Reference Include=""System.ComponentModel.DataAnnotations"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Runtime.Serialization"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.Xml"" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include = ""JsonSubTypes"" Version=""2.0.1"" />
    <PackageReference Include = ""RestSharp"" Version=""110.2.0"" />
    <PackageReference Include = ""Polly"" Version=""7.2.4"" />
    <PackageReference Include = ""System.ComponentModel.Annotations"" Version=""4.5.0"" />
    <PackageReference Include = ""System.Runtime.Serialization.Primitives"" Version=""4.3.0"" />
  </ItemGroup>
</Project>";
    }
}