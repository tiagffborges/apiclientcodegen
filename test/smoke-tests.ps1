. .\utilities.ps1
# Install-DotNetRuntimes

############################
## OpenAPI Spec v2 (JSON) ##
############################

Remove-Item ./**/*Output.cs
Download-SwaggerPetstore -Version "v2" -Format "json"

Generate-CodeThenBuild -ToolName "AutoRest" -Format "json" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "NSwag" -Format "json" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "SwaggerCodegen" -Format "json" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "OpenApiGenerator" -Format "json" -Method "dotnet-run"

Remove-Item Swagger.*
Remove-Item ./**/*Output.cs


############################
## OpenAPI Spec v2 (YAML) ##
############################

Remove-Item ./**/*Output.cs
Download-SwaggerPetstore -Version "v2" -Format "yaml"

Generate-CodeThenBuild -ToolName "AutoRest" -Format "yaml" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "NSwag" -Format "yaml" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "SwaggerCodegen" -Format "yaml" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "OpenApiGenerator" -Format "yaml" -Method "dotnet-run"

Remove-Item Swagger.*
Remove-Item ./**/*Output.cs

############################
## OpenAPI Spec v3 (JSON) ##
############################

Remove-Item ./**/*Output.cs
Download-SwaggerPetstore -Version "v3" -Format "json"

Generate-CodeThenBuild -ToolName "NSwag" -Format "json" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "SwaggerCodegen" -Format "json" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "OpenApiGenerator" -Format "json" -Method "dotnet-run"

Remove-Item Swagger.*
Remove-Item ./**/*Output.cs

############################
## OpenAPI Spec v3 (YAML) ##
############################

Remove-Item ./**/*Output.cs
Download-SwaggerPetstore -Version "v3" -Format "yaml"

Generate-CodeThenBuild -ToolName "NSwag" -Format "yaml" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "SwaggerCodegen" -Format "yaml" -Method "dotnet-run"
Generate-CodeThenBuild -ToolName "OpenApiGenerator" -Format "yaml" -Method "dotnet-run"

Remove-Item Swagger.*
Remove-Item ./**/*Output.cs

Write-Host "`r`n"