# Dynamo Development Starter Kit

The set up of a project aiming the [Dynamo](http://dynamobim.org/) platform can be hard and frustating if not used to it. This project is a Visual Studio Extension (VSIX) containing templates for the set up of [Dynamo](http://dynamobim.org/) package projects, providing boilerplates so you can just start developing your stuff.

## Installation

Having the project downloaded and Visual Studio totally closed, execute the `.vsix` file within the `dist` folder. This pops up a window that will install the extension along with templates.

![Installing](assets/images/installation.gif)

## Usage

- Within Visual Studio, create a new project. Under Visual C#, you should have two new templates: `Dynamo Template - ZeroTouch` and `Dynamo Template - Explicit Node`.
- Fill in the parameters of your projects, seing on the panel on the rigth a preview of the `pkg.json` file that will get generated.
- Once accepted, it will pull all the necessary Nuget packages.
- Remember to set all references to `copy local = false` before start debugging.

![Usage](assets/images/usage.gif)
