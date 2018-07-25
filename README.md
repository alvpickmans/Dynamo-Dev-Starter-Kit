# Dynamo Development Starter Kit

The set up of a project aiming the [Dynamo](http://dynamobim.org/) platform can be hard and frustating if not use to it. This project is a Visual Studio Extension (VSIX) containing templates for the set up of Dynamo package projects, providing boilerplates so you can just start developing your stuff.

## Getting Started

These instructions will get you started on how to install the VSIX and start developing your own Dynamo packages. 
*Bear in mind these templates are just one of many ways of setting up Visual Studio to seamlessly create a base structures.*

### Prerequisites

- [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/)
- [Dynamo Visual Programming](http://dynamobim.org/download/)

### Installation

Having the project downloaded and Visual Studio totally closed, execute the `.vsix` file within the `/dist` folder. This pops up a window that will install the extension along with templates.

![Installing](assets/images/installation.gif)

### Usage

*Note: Bear in mind that the templates distributed on this extension are by no means mandatory and just represent my personal way of structuring projects. These templates can be modified whene [building the extension from source](#building-from-source)*

![Usage](assets/images/usage.gif)

- Within Visual Studio, create a new project. Under Visual C#, you should have two new templates: `Dynamo Template - ZeroTouch` and `Dynamo Template - Explicit Nodes`.
- Fill in the parameters of your projects. The panel on the rigth displays a preview of the `pkg.json` file that will get generated.
- Once accepted, it will download all the necessary Dynamo Nuget packages and you'll be ready to start coding:
    - **ZeroTouch Template**: Before start debugging, remember to set `copy local` property to `false`.
    - **Explicit Nodes Template**: Similar to ZeroTouch, remember to set `copy local = false` on both projects. After, add the reference to the project `{YourProjectName}` to `{YourProjectName}.UI`, keeping this with `copy local = true`.
- Finally when compiling, a package folder will be created at `{YourSolutionName}/dist/{YourProjectName}`. If configuration mode is on `Debug`, this package folder will be copied to the Dynamo packages folder, following the below snippet on the `{YourProjectName}.UI.csproj` file:

```xml
<PropertyGroup>
    <PackageName>Sample Project</PackageName>
    <VersionFolder>2.0</VersionFolder> 
    <PackageFolder>$(SolutionDir)dist\$(PackageName)\</PackageFolder>
    <BinFolder>$(PackageFolder)bin\</BinFolder>
    <ExtraFolder>$(PackageFolder)extra\</ExtraFolder>
    <DyfFolder>$(PackageFolder)dyf\</DyfFolder>
  </PropertyGroup>
  <Target Name="AfterBuild">
    <ItemGroup>
      <Dlls Include="$(OutDir)*.dll" />
      <Pdbs Include="$(OutDir)*.pdb" />
      <Xmls Include="$(OutDir)*.xml" />
      <Xmls Include="$(ProjectDir)manifests\*.xml" />
      <PackageJson Include="$(ProjectDir)manifests\pkg.json" />
    </ItemGroup>
    <Copy SourceFiles="@(Dlls)" DestinationFolder="$(BinFolder)" />
    <Copy SourceFiles="@(Pdbs)" DestinationFolder="$(BinFolder)" />
    <Copy SourceFiles="@(Xmls)" DestinationFolder="$(BinFolder)" />
    <Copy SourceFiles="@(PackageJson)" DestinationFolder="$(PackageFolder)" />
    <MakeDir Directories="$(ExtraFolder)" Condition="!Exists($(ExtraFolder))" />
    <MakeDir Directories="$(DyfFolder)" Condition="!Exists($(DyfFolder))"  />
    <CallTarget Condition="'$(Configuration)' == 'Debug'" Targets="PackageDeploy" />
  </Target>
  <Target Name="PackageDeploy">
    <ItemGroup>
      <SourcePackage Include="$(PackageFolder)**\*" />
    </ItemGroup>
    <PropertyGroup>
      <DynamoCore>$(AppData)\Dynamo\Dynamo Core\$(VersionFolder)\packages</DynamoCore>
      <DynamoRevit>$(AppData)\Dynamo\Dynamo Revit\$(VersionFolder)\packages</DynamoRevit>
    </PropertyGroup>
    <!--Copying to Package Folder-->
    <Copy SourceFiles="@(SourcePackage)" Condition="Exists($(DynamoCore))" DestinationFolder="$(DynamoCore)\$(PackageName)\%(RecursiveDir)" />
    <Copy SourceFiles="@(SourcePackage)" Condition="Exists($(DynamoRevit))" DestinationFolder="$(DynamoRevit)\$(PackageName)\%(RecursiveDir)" />
  </Target>
```


## Building from Source

In order to build the project from source, Visual Studio SDK must be [installed](https://msdn.microsoft.com/en-us/library/mt683786.aspx?f=255&MSPPError=-2147217396).
The solution has three projects:
- **DynamoDev.StarterKitExtension**: Handling the VSIX extension and contains the Package Definition window.
- **DynamoDev.ZeroTouch**: Contains the template to deploy a ZeroTouch package (not requiring custom UI).
- **DynamoDev.ExplicitNodes**: Contains the template to deploy a Dynamo package whith nodes requiring UI customization.

On debugging, another instance of Visual Studion will open and the extension can be used.
On Release configuration mode, after being built the corresponding files will be copied to the `/dist` folder.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
