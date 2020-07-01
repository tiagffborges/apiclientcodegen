﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Windows.Forms;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.CustomTool
{
    [ExcludeFromCodeCoverage]
    public abstract class CustomToolSetter<T>
        : ICommandInitializer
        where T : SingleFileCodeGenerator
    {
        protected abstract int CommandId { get; }
        protected Guid CommandSet { get; } = new Guid("C292653B-5876-4B8C-B672-3375D8561881");
        protected virtual bool SupportsYaml { get; } = true;

        public Task InitializeAsync(
            AsyncPackage package,
            CancellationToken token)
            => package.SetupCommandAsync(
                CommandSet,
                CommandId,
                OnExecuteAsync,
                token);

        private async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = dte.SelectedItems.Item(1).ProjectItem;
            if (!SupportsYaml && item.Name.EndsWith("yaml", StringComparison.OrdinalIgnoreCase))
            {
                const string message = "Specified code generator doesn't support YAML files";
                MessageBox.Show(message, "Not Supported");
                Trace.WriteLine(message);
                return;
            }

            item.Properties.Item("CustomTool").Value = typeof(T).Name;

            var name = typeof(T).Name.Replace("CodeGenerator", string.Empty);
            Trace.WriteLine($"Generating code using {name}");

            var project = ProjectExtensions.GetActiveProject(dte);

            await project.InstallMissingPackagesAsync(
                package,
                typeof(T).GetSupportedCodeGenerator());
        }
    }
}