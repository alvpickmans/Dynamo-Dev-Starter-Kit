using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using NuGet;
using NuGet.VisualStudio;

namespace DynamoDev.StarterKitExtension
{
    public static class Helpers
    {
        private static void WriteToOutput(DTE dte, string output)
        {
            var window = dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            var outputWindow = (OutputWindow)window.Object;
            OutputWindowPane pane;
            try
            {
                pane = outputWindow.OutputWindowPanes.Item("DynamoStarterKit");
            }
            catch
            {
                pane = outputWindow.OutputWindowPanes.Add("DynamoStarterKit");
            }
            pane.Activate();
            pane.OutputString(output);
            pane.OutputString("\n");

        }

        //https://stackoverflow.com/questions/36143663/create-a-visual-studio-project-template-that-pulls-nuget-references-from-online
        public static void InstallPackages(Project project)
        {
            ProjectItem config = project.ProjectItems.Item("packages.init");
            DTE dte = project.DTE;
            if (config == null && config.FileCount == 0)
            {
                return;
            }
            var path = config.FileNames[0];
            WriteToOutput(dte, "Installing packages for " + project.Name);

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            IVsPackageInstallerServices installerServices =
                 componentModel.GetService<IVsPackageInstallerServices>();
            var file = new PackageReferenceFile(path);

            foreach(PackageReference pRef in file.GetPackageReferences())
            {
                if (!installerServices.IsPackageInstalled(project, pRef.Id))
                {
                    WriteToOutput(dte , String.Format("Installing {0}, version {1}", pRef.Id, pRef.Version.Version.ToString()));

                    var installer = componentModel.GetService<IVsPackageInstaller>();
                    installer.InstallPackage(
                        "All",
                        project,
                        pRef.Id,
                        pRef.Version.Version,
                        false);
                }
            }
            config.Delete();
        }

    }
}
