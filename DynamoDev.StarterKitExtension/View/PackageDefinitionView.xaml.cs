using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace DynamoDev.StarterKitExtension
{
    /// <summary>
    /// Interaction logic for PackageDefinitionView.xaml
    /// </summary>
    public partial class PackageDefinitionView : Window
    {
        private PackageDefinitionViewModel viewModel;

        public PackageDefinitionView(PackageDefinitionViewModel vm)
        {
            this.viewModel = vm;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {   
            if (e.Key == Key.Escape)
            {

            }
        }
    }
}
