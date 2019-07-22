using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;
using EnvDTE;

namespace DynamoDev.StarterKitExtension
{
    public class WizardRoot : IWizard
    {
        private const string WIZARD_TITLE = "Dynamo Dev Starter Kit";
        public static Dictionary<string, string> GlobalDictionary = new Dictionary<string, string>();

        private string InstanceTitle { get; set; }
        private bool IsSingleProjectWizard = true;
        private PackageDefinitionView view;
        private string DynamoSandbox2path = @"C:\Program Files\Dynamo\Dynamo Core\2\DynamoSandbox.exe";
        private string DynamoSandbox1path = @"C:\Program Files\Dynamo\Dynamo Revit\{0}\DynamoSandbox.exe";

        // This method is called before opening any item that   
        // has the OpenInEditor attribute.  
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            if (IsSingleProjectWizard)
                Helpers.RestorePackages(project);
        }

        // This method is only called for item templates,  
        // not for project templates.  
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.  
        public void RunFinished()
        {
        }

        public void AddReplacement(Dictionary<string, string> dict, string key, string value)
        {
            dict.Add(key, value);
            GlobalDictionary[key] = value;
        }

        public string GetAssemblyData(string name, string version)
        {
            if (name == String.Empty || version == String.Empty)
            {
                return String.Empty;
            }
            else
            {
                return String.Format("\"{0}, Version={1}, Culture=neutral, PublicKeyToken=null\"", name, version);
            }
        }

        public void RunStarted(object automationObject,
          Dictionary<string, string> replacementsDictionary,
          WizardRunKind runKind, object[] customParams)
        {
            GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"];
            string destinationDirectory = replacementsDictionary["$destinationdirectory$"];
            string projectType = replacementsDictionary["$projecttype$"];
            this.InstanceTitle = $"{WIZARD_TITLE} - {projectType}";

            PackageDefinitionViewModel viewModel = new PackageDefinitionViewModel();
            viewModel.PackageName = replacementsDictionary["$safeprojectname$"];
            viewModel.AddAssembly(replacementsDictionary["$safeprojectname$"], "1.0.0.0");

            if (runKind == WizardRunKind.AsMultiProject)
            {
                viewModel.AddAssembly(replacementsDictionary["$safeprojectname$"] + ".UI", "1.0.0.0");
                IsSingleProjectWizard = false;
            }

            view = new PackageDefinitionView(viewModel)
            {
                Title = this.InstanceTitle,
                DataContext = viewModel
            };

            foreach (var version in viewModel.dynamoEngineVersions.Keys)
            {
                view.engineVersions.Items.Add(version);
            }
            view.engineVersions.SelectedIndex = 0;

            view.Closed += (sender, args) =>
            {
                if (!viewModel.IsCancelled)
                {
                    var versionNumbers = viewModel.EngineVersion.Split('.');
                    var versionFolder = string.Join(".", new string[2] { versionNumbers[0], versionNumbers[1] });
                    var sandBoxPath = (versionNumbers[0] == "2") ? DynamoSandbox2path : String.Format(DynamoSandbox1path, versionFolder);
                    var assemblies = viewModel.assemblies.ToList();
                    string assemblyMain = (assemblies.Count >= 1) ? GetAssemblyData(assemblies[0].Key, assemblies[0].Value) : String.Empty;
                    string assemblyFunctions = (assemblies.Count >= 2) ? GetAssemblyData(assemblies[1].Key, assemblies[1].Value) : String.Empty;

                    AddReplacement(replacementsDictionary, "$packageName$", viewModel.PackageName);
                    AddReplacement(replacementsDictionary, "$packageVersion$", viewModel.PackageVersion);
                    AddReplacement(replacementsDictionary, "$packageDescription$", viewModel.PackageDescription);
                    AddReplacement(replacementsDictionary, "$dynamoVersion$", viewModel.DynamoVersion);
                    AddReplacement(replacementsDictionary, "$engineVersion$", viewModel.EngineVersion);
                    AddReplacement(replacementsDictionary, "$versionFolder$", versionFolder);
                    AddReplacement(replacementsDictionary, "$siteUrl$", viewModel.SiteUrl);
                    AddReplacement(replacementsDictionary, "$repoUrl$", viewModel.RepoUrl);
                    AddReplacement(replacementsDictionary, "$startProgramPath$", sandBoxPath);
                    AddReplacement(replacementsDictionary, "$nodeLibraries$", viewModel.NodeLibraries);
                    AddReplacement(replacementsDictionary, "$assemblyMain$", assemblyMain);
                    AddReplacement(replacementsDictionary, "$assemblyFunctions$", assemblyFunctions);
                    AddReplacement(replacementsDictionary, "$guidMain$", new Guid().ToString());
                    AddReplacement(replacementsDictionary, "$guidUI$", new Guid().ToString());
                }
            };

            view.Closing += (sender, args) =>
            {
                if (!viewModel.forceClose)
                {
                    var result = MessageBox.Show("Do you wish to stop creating the project?", this.InstanceTitle, MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                        viewModel.IsCancelled = true;
                    else
                        args.Cancel = true;
                }

            };

            view.btn_Accept.Click += (sender, args) =>
            {
                if (!viewModel.IsEngineVersionSet())
                {
                    MessageBox.Show("An Engine Version must be selected.", this.InstanceTitle);
                }
                else
                {
                    var result = MessageBox.Show("Are you happy with the package? You will be able to change the parameters later on.", this.InstanceTitle, MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        viewModel.forceClose = true;
                        view.Close();
                    }
                }
            };

            view.ShowDialog();

            try
            {
                if (viewModel.IsCancelled)
                {
                    throw new WizardBackoutException();
                }
            }
            catch
            {

                if (System.IO.Directory.Exists(destinationDirectory))
                {
                    System.IO.Directory.Delete(destinationDirectory, true);
                }

                throw;
            }
        }

        // This method is only called for item templates,  
        // not for project templates.  
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
