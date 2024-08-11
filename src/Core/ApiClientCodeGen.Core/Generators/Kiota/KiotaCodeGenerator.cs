﻿using System;
using System.IO;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;

namespace Rapicgen.Core.Generators.Kiota;

public class KiotaCodeGenerator(
    string swaggerFile,
    string defaultNamespace,
    IProcessLauncher processLauncher,
    IDependencyInstaller dependencyInstaller,
    IKiotaOptions options) : ICodeGenerator
{
    private const string KiotaLockFile = "kiota-lock.json";
    private const string Command = "kiota";

    public string GenerateCode(IProgressReporter? pGenerateProgress)
    {
        pGenerateProgress?.Progress(10);
        dependencyInstaller.InstallKiota();

        string swaggerFilename = new FileInfo(swaggerFile).Name;
        if (swaggerFilename == KiotaLockFile)
        {
            pGenerateProgress?.Progress(40);
            RunKiotaUpdate();

            pGenerateProgress?.Progress(100);
            return GeneratedCode.PrefixAutogeneratedCodeHeader(
                string.Empty,
                "Kiota",
                "v1.17.0");
        }

        pGenerateProgress?.Progress(30);
        string outputFolder = options.GenerateMultipleFiles
            ? Path.GetDirectoryName(swaggerFile)
            : Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        pGenerateProgress?.Progress(40);
        RunKiotaGenerate(outputFolder);

        if (!options.GenerateMultipleFiles &&
            File.Exists(Path.Combine(Path.GetDirectoryName(swaggerFile)!, KiotaLockFile)))
        {
            File.Copy(
                swaggerFile,
                Path.Combine(outputFolder, swaggerFilename),
                true);

            File.Copy(
                Path.Combine(Path.GetDirectoryName(swaggerFile)!, KiotaLockFile),
                Path.Combine(outputFolder, KiotaLockFile),
                true);

            pGenerateProgress?.Progress(60);

            var arguments = $" update -o \"{outputFolder}\"";
            using var context = new DependencyContext("Kiota", $"{Command} {arguments}");
            processLauncher.Start(Command, arguments, outputFolder);
            context.Succeeded();
        }

        pGenerateProgress?.Progress(80);
        string output = string.Empty;

        if (!options.GenerateMultipleFiles)
        {
            output = CSharpFileMerger.MergeFiles(outputFolder);

            var kiotaConfigFile = Path.Combine(outputFolder, KiotaLockFile);
            if (File.Exists(kiotaConfigFile))
            {
                File.Copy(
                    kiotaConfigFile,
                    Path.Combine(Path.GetDirectoryName(swaggerFile)!, KiotaLockFile),
                    true);
            }
        }

        pGenerateProgress?.Progress(100);
        return GeneratedCode.PrefixAutogeneratedCodeHeader(
            output,
            "Kiota",
            "v1.17.0");
    }

    private void RunKiotaGenerate(string outputFolder)
    {
        var arguments = $" generate -l CSharp -d \"{swaggerFile}\" -o \"{outputFolder}\" -n {defaultNamespace}";
        using var context = new DependencyContext("Kiota", $"{Command} {arguments}");
        processLauncher.Start(Command, arguments);
        context.Succeeded();
    }

    private void RunKiotaUpdate()
    {
        var arguments = $" update -o \"{Path.GetDirectoryName(swaggerFile)!}\"";
        using var context = new DependencyContext("Kiota", $"{Command} {arguments}");
        processLauncher.Start(Command, arguments);
        context.Succeeded();
    }
}