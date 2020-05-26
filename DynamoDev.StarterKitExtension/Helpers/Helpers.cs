//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EnvDTE;
//using Microsoft.VisualStudio.ComponentModelHost;
//using Microsoft.VisualStudio.Shell;
//using NuGet;
//using NuGet.VisualStudio;

//namespace DynamoDev.StarterKitExtension
//{
//    public static class Helpers
//    {
//        private static IComponentModel componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

//        public static void RestorePackages(Project project)
//        {
//            var restorerServices = componentModel.GetService<IVsPackageRestorer>();

//            if (restorerServices.IsUserConsentGranted())
//                restorerServices.RestorePackages(project);
//        }
//    }
//}
