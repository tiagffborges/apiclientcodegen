﻿using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Options.Refitter;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Rapicgen.Options.Refitter
{
    [ExcludeFromCodeCoverage]
    [ComVisible(true)]
    public class RefitterOptionsPage : DialogPage, IRefitterOptions
    {
        public const string Name = "Refitter";

        [Category(Name)]
        [DisplayName("Generate Contracts")]
        [Description("Set this to FALSE to skip generating the contract types (default: TRUE)")]
        public bool GenerateContracts { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate XML code comments")]
        [Description("Set this to FALSE to skip generating XML doc style code comments (default: TRUE)")]
        public bool GenerateXmlDocCodeComments { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate <auto-generated> Header")]
        [Description("Set this to FALSE to skip generating <auto-generated> headers in C# files (default: TRUE)")]
        public bool AddAutoGeneratedHeader { get; set; }

        [Category(Name)]
        [DisplayName("Return IApiResponse<T>")]
        [Description("Set this to TRUE to wrap the returned the contract types in IApiResponse<T> (default: FALSE)")]
        public bool ReturnIApiResponse { get; set; }
    }
}
