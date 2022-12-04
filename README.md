to Pack: nuget.exe pack tixone.Template.NetCoreConsole.nuspec

To install a template from a NuGet package stored at nuget.org: 
	dotnet new install Tixone.NtCore.Console.App.Template::<Version>

	dotnet new --install package.nupkg

To uninstall a template: 
	dotnet new uninstall ncapp

To check if you template has been installed successfully: 
	dotnet new -l

To use your templates with the .NET CLI: 
	dotnet new ncapp -n <PROJECT_NAME> -o <FOLDER>