﻿using System;
using System.IO;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;

namespace Rapicgen.Core.Generators.Kiota;

public class KiotaCodeGenerator : ICodeGenerator
{
    private readonly string swaggerFile;
    private readonly string defaultNamespace;
    private readonly IProcessLauncher processLauncher;
    private readonly IDependencyInstaller dependencyInstaller;

    public KiotaCodeGenerator(
        string swaggerFile,
        string defaultNamespace,
        IProcessLauncher processLauncher,
        IDependencyInstaller dependencyInstaller)
    {
        this.swaggerFile = swaggerFile;
        this.defaultNamespace = defaultNamespace;
        this.processLauncher = processLauncher;
        this.dependencyInstaller = dependencyInstaller;
    }

    public string GenerateCode(IProgressReporter? pGenerateProgress)
    {
        pGenerateProgress?.Progress(10);
        dependencyInstaller.InstallKiota();

        pGenerateProgress?.Progress(30);
        var outputFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputFolder);

        pGenerateProgress?.Progress(40);
        const string command = "kiota";
        var arguments = $" generate -l CSharp -d {swaggerFile} -o {outputFolder} -n {defaultNamespace}";
        using var context = new DependencyContext("Kiota", $"{command} {arguments}");
        processLauncher.Start(command, arguments);
        context.Succeeded();

        pGenerateProgress?.Progress(80);
        var output = CSharpFileMerger.MergeFiles(outputFolder);

        pGenerateProgress?.Progress(100);
        return GeneratedCode.PrefixAutogeneratedCodeHeader(output, "Kiota", "v1.11.1");
    }
}