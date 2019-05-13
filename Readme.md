# Threax.PackageUpdate
This library provides helper classes to check for updates in nuget and npm packages. You can check multiple upstream sources. There are libraries to read from the official NuGet.org, Npmjs.com and Azure DevOps.

This library just helps you find updates to upstream packages. It does not help you install or update them in your projects.

## NuGet Packages
This library is split into several NuGet packages since there are lots of dependencies to make each one work even though they are relatively simple. You don't want to have to include a bunch of dependencies you don't really need in your project.

* Threax.PackageUpdate - The core library.
* Threax.PackageUpdate.AzureDevOps - Check for updates for both npm and nuget from Azure DevOps package feeds.
* Threax.PackageUpdate.Npm - Check for updates from npmjs.com.
* Threax.PackageUpdate.Nuget - Check for updates from nuget.org.

## Checking for Updates
The main class that will look for updates is UpdateChecker. The following examples show how to build one of these up. You can add or remove sources as you need.

### NuGet
This example shows how to read NuGet packages from Nuget.org.
```
var nugetVersionChecker = new UpdateChecker(new NugetVersionComparer(), new NugetOrgVersionSource());
var latest = await nugetVersionChecker.GetLatestVersion("Threax.AspNetCore.Halcyon.Ext");
```

### NpmJs
This example shows how to read Npm packages from Npmjs.org
```
using (var npmjsVersionSource = new NpmjsVersionSource())
{
    var npmVersionChecker = new UpdateChecker(new NpmVersionComparer(), npmjsVersionSource);
    var update = await npmVersionChecker.GetLatestVersion("htmlrapier");
}
```
The NpmJsVersionSource is Disposable, so you must be sure to dispose of it when you are done with it. Be sure to keep it open while you check for updates.

### Azure DevOps
This example shows how to read NuGet packages from an upstream Azure DevOps and NuGet.org.
```
// Connect to Azure DevOps Services
VssConnection connection = new VssConnection(collectionUri, new VssBasicCredential("", accessToken));
using (var feedClient = await connection.GetClientAsync<FeedClient>())
{
    feedClient.Org = "YourOrg";
    var devopsNuget = new DevOpsVersionSource(DevOpsVersionSource.NuGet, packages);
    var nugetOrgVersionSource = new NugetOrgVersionSource();
    var nugetVersionChecker = new UpdateChecker(new NugetVersionComparer(), devopsNuget, nugetOrgVersionSource);
    var latest = await nugetVersionChecker.GetLatestVersion("Threax.AspNetCore.Halcyon.Ext");
}
```
This will check both Azure DevOps and Nuget.org for package updates. You can add as many package sources as you need and the highest non preview version from all of them will be found and returned.