using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DynamoDev.StarterKitExtension
{
    public partial class PackageDefinitionViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Private Properties
        private string packageName = String.Empty;
        private int versionMajor = 0;
        private int versionMinor = 0;
        private int versionPatch = 1;
        private string packageDescription = String.Empty;
        private string dynamoVersion = String.Empty;
        private string engineVersion = String.Empty;
        private string siteUrl = String.Empty;
        private string repoUrl = String.Empty;
        internal bool forceClose = false;
        internal Dictionary<string,string> dynamoEngineVersions = new Dictionary<string, string>()
        {
            { "2.1.0.7465", "2.1.0.7465" },
            { "2.1.0.7451", "2.1.0.7451" },
            { "2.1.0.7436", "2.1.0.7436" },
            { "2.1.0.7430", "2.1.0.7430" },
            { "2.0.2.6986", "2.0.2.6986" },
            { "2.0.1.5055", "2.0.1.5055" },
            { "2.0.1.4955", "2.0.1.4955" },
            { "2.0.1.4845", "2.0.1.4845" },
            { "2.0.0.4714", "2.0.0.4714" },
            { "2.0.0", "2.0.0.4604" }
        };

        #endregion

        #region Public Properties
        /// <summary>
        /// Dictionary hosting the assemblies name as its key and their version as associated value
        /// </summary>
        public Dictionary<string, string> assemblies = new Dictionary<string, string>();
        public bool IsCancelled = false;
        
        public string PackageName
        {
            get { return packageName; }
            set
            {
                packageName = value;
                NotifyPropertyChanged("PackageName");
            }
        }

        public string VersionMajor
        {
            get { return versionMajor.ToString(); }
            set
            {
                versionMajor = Convert.ToInt32(value);
                NotifyPropertyChanged("VersionMajor");
                NotifyPropertyChanged("PackageVersion");
            }
        }

        public string VersionMinor
        {
            get { return versionMinor.ToString(); }
            set
            {
                versionMinor = Convert.ToInt32(value);
                NotifyPropertyChanged("VersionMinor");
                NotifyPropertyChanged("PackageVersion");
            }
        }

        public string VersionPatch
        {
            get { return versionPatch.ToString(); }
            set
            {
                versionPatch = Convert.ToInt32(value);
                NotifyPropertyChanged("VersionPatch");
                NotifyPropertyChanged("PackageVersion");
            }
        }

        public string PackageVersion
        {
            get { return String.Format("{0}.{1}.{2}", VersionMajor, VersionMinor, VersionPatch); }
        }

        public string PackageDescription
        {
            get { return packageDescription; }
            set
            {
                packageDescription = value;
                NotifyPropertyChanged("PackageDescription");
            }
        }

        public string DynamoVersion
        {
            get { return dynamoVersion; }
            set
            {
                dynamoVersion = value;
                EngineVersion = dynamoEngineVersions[dynamoVersion];
                NotifyPropertyChanged("DynamoVersion");
            }
        }

        public string EngineVersion
        {
            get { return engineVersion; }
            set
            {
                engineVersion = value;
                NotifyPropertyChanged("EngineVersion");
            }
        }

        public string SiteUrl
        {
            get { return siteUrl; }
            set
            {
                siteUrl = value;
                NotifyPropertyChanged("SiteUrl");
            }
        }

        public string RepoUrl
        {
            get { return repoUrl; }
            set
            {
                repoUrl = value;
                NotifyPropertyChanged("RepoUrl");
            }
        }

        public string NodeLibraries
        {
            get
            {
                string nodes = String.Empty;
                string lastKey = assemblies.Keys.Last();
                foreach(var assembly in assemblies)
                {
                    nodes += String.Format("\"{0}, Version={1}, Culture=neutral, PublicKeyToken=null\"", assembly.Key, assembly.Value);
                    if (!assembly.Key.Equals(lastKey))
                    {
                        nodes += "," + Environment.NewLine;
                    }
                }
                return nodes;
            }
        }

        #endregion

        #region Constructor

        public PackageDefinitionViewModel()
        {
        }


        #endregion

        #region Public Methods

        public void AddAssembly(string name, string version)
        {
            this.assemblies.Add(name, version);
        }

        public bool IsEngineVersionSet()
        {
            return this.engineVersion != String.Empty;
        }

        #endregion
    }
}
