﻿using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.NuGet
{
    [TestClass]
    public class SupportedCodeGeneratorExtensionsTests
    {
        [TestMethod]
        public void GetDependencies_NSwag_Returns_NotEmpty()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_AutoRest_Returns_NotEmpty()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_Swagger_Returns_NotEmpty()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_OpenApi_Returns_NotEmpty()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .NotBeNullOrEmpty();

        [TestMethod]
        public void GetDependencies_Swagger_OpenApi_Same_Dependencies()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .BeEquivalentTo(
                    SupportedCodeGenerator.OpenApi
                            .GetDependencies());

        [TestMethod]
        public void GetDependencies_NSwag_Contains_NewtonsoftJson()
            => SupportedCodeGenerator.NSwag
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Newtonsoft.Json");

        [TestMethod]
        public void GetDependencies_AutoRest_Contains_RestClientRuntime()
            => SupportedCodeGenerator.AutoRest
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "Microsoft.Rest.ClientRuntime");

        [TestMethod]
        public void GetDependencies_Swagger_Contains_RestSharp()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "RestSharp");

        [TestMethod]
        public void GetDependencies_Swagger_Contains_JsonSubTypes()
            => SupportedCodeGenerator.Swagger
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "JsonSubTypes");

        [TestMethod]
        public void GetDependencies_OpenApi_Contains_RestSharp()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "RestSharp");

        [TestMethod]
        public void GetDependencies_OpenApi_Contains_JsonSubTypes()
            => SupportedCodeGenerator.OpenApi
                .GetDependencies()
                .Should()
                .Contain(c => c.Name == "JsonSubTypes");
    }
}
