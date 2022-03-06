﻿using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AsyncPackageExtensions
    {
        public static async Task SetupCommandAsync(
            this AsyncPackage package,
            Guid commandSet,
            int commandId,
            Func<AsyncPackage, Task> func,
            CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? CancellationToken.None;
            await package.JoinableTaskFactory
                .SwitchToMainThreadAsync(token);
            
            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as IMenuCommandService;
            if (commandService == null)
                return;

            var menuCommand = new MenuCommand(
                async (sender, e) => await InvokeAsync(package, func), 
                new CommandID(commandSet, commandId));

            commandService.AddCommand(menuCommand);
        }

        public static async Task<Project> GetActiveProjectAsync(this AsyncPackage package)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                var dteTask = package.GetServiceAsync(typeof(EnvDTE.DTE));
                var Dte = await dteTask as EnvDTE.DTE;
                return GetActiveProject(Dte);
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Trace.WriteLine("Error getting the active project" + ex);
            }

            return null;
        }

        private static Project GetActiveProject(this DTE Dte)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                if (Dte.ActiveSolutionProjects is Array activeSolutionProjects && activeSolutionProjects.Length > 0)
                    return activeSolutionProjects.GetValue(0) as Project;

                var doc = Dte.ActiveDocument;

                if (doc != null && !string.IsNullOrEmpty(doc.FullName))
                {
                    var item = Dte.Solution?.FindProjectItem(doc.FullName);

                    if (item != null)
                        return item.ContainingProject;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.TrackError(ex);
                Trace.WriteLine("Error getting the active project" + ex);
            }

            return null;
        }

        private static async Task InvokeAsync(AsyncPackage package, Func<AsyncPackage, Task> func)
        {
            try
            {
                await func.Invoke(package);
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Trace.TraceError(e.ToString());
            }
        }
    }
}