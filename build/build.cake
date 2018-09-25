//////////////////////////////////////////////////////////////////////
// Tools
//////////////////////////////////////////////////////////////////////
#tool "nuget:?package=GitVersion.CommandLine&version=3.6.5"

//////////////////////////////////////////////////////////////////////
// Variables
//////////////////////////////////////////////////////////////////////
var configuration = Argument("configuration", "Release");
var target = Argument("Target", "Default");
GitVersion versionInfo = null;
var projectFolder = "./../Tynamix.ObjectFiller/";
var projectFile = projectFolder + "Tynamix.ObjectFiller.csproj";

//////////////////////////////////////////////////////////////////////
// Tasks
//////////////////////////////////////////////////////////////////////
Task("Build")
	.IsDependentOn("Version")
	.Does(() =>
	{
		MSBuild(projectFile, new MSBuildSettings 
		{
			Configuration = configuration,
			Restore = true
		});
	});
	
Task("Version")
	.Does(() =>
	{
		versionInfo = GitVersion(new GitVersionSettings {
			UpdateAssemblyInfo = true
		});

		Information("Version: " + versionInfo.AssemblySemVer);
	});
	
Task("Default")
	.IsDependentOn("Build");
	
RunTarget(target);