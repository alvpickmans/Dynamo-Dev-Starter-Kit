using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;
using EnvDTE;


namespace DynamoDev.StarterKitExtension
{
    public class WizardCustomNodeChild : IWizard
    {

        // This method is called before opening any item that   
        // has the OpenInEditor attribute.  
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            Helpers.InstallPackages(project);
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

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            //replacementsDictionary.Add("$saferootprojectname$", WizardCustomNode.GlobalDictionary["$saferootprojectname$"]);
            foreach(var kv in WizardRoot.GlobalDictionary)
            {
                replacementsDictionary.Add(kv.Key, kv.Value);
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
