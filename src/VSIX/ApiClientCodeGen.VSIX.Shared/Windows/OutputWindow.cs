﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread

namespace Rapicgen.Windows
{
    [ExcludeFromCodeCoverage]
    public static class OutputWindow
    {
        private static string? name;
        private static IVsOutputWindowPane? pane;
        private static IVsOutputWindow? output;

        public static void Initialize(IServiceProvider provider, string? outputSource)
        {
            if (output != null)
                return;

            ThreadHelper.ThrowIfNotOnUIThread();
            output = (IVsOutputWindow)provider.GetService(typeof(SVsOutputWindow));
            Assumes.Present(output);
            name = outputSource;
        }

        public static void Log(object message)
        {
            try
            {
                Initialize(VsPackage.Instance, VsPackage.VsixName);

                if (EnsurePane())
                    pane?.OutputStringThreadSafe($"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch
            {
                // ignored
            }
        }

        private static bool EnsurePane()
        {
            if (pane != null)
                return true;

            var guid = new Guid("C7783FF4-55A9-422F-A3DD-4EA81E5CB6BB");
            output?.CreatePane(ref guid, name, 1, 1);
            output?.GetPane(ref guid, out pane);
            return pane != null;
        }
    }
}
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread