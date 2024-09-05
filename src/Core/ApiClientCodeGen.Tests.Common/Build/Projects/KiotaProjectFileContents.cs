﻿namespace ApiClientCodeGen.Tests.Common.Build.Projects
{
    public static class KiotaProjectFileContents
    {
        public const string NetCoreApp =
            """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>net8.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.Kiota.Abstractions" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Http.HttpClientLibrary" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Form" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Json" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Multipart" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Text" Version="1.12.4" />
              </ItemGroup>
            </Project>
            """;

        public const string NetStandardLibrary =
            """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>netstandard2.1</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.Kiota.Abstractions" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Http.HttpClientLibrary" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Form" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Json" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Multipart" Version="1.12.4" />
                <PackageReference Include="Microsoft.Kiota.Serialization.Text" Version="1.12.4" />
              </ItemGroup>
            </Project>
            """;
    }
}