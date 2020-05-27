using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
/* dynamo directives */
using Dynamo.Controls;
using Dynamo.Wpf;
using Dynamo.ViewModels;
using System.Windows.Controls;
using System.Windows;
using $projectname$.Functions;
using $projectname$.Nodes;


namespace $projectname$.Views
{
  public class HelloUINodeView : INodeViewCustomization<HelloUINode>
  {
    private DynamoViewModel _dynamoVM;
    private NodeView _nodeview;
    private HelloUINode _model;

  public void CustomizeView(HelloUINode model, NodeView nodeView)
    {
    _dynamoVM = nodeView.ViewModel.DynamoViewModel;
    _nodeview = nodeView;
    _model = model;

    var button = new Button()
    {
      Content = "Hello?",
      Width = 80,
      Margin = new System.Windows.Thickness(10, 0, 10, 0)
    };

    button.Click += Button_Click;
    nodeView.inputGrid.Children.Add(button);
  }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var message = "Hello there!".ToUpperCaseCustom();
        MessageBox.Show(message);
    }

    public void Dispose()
    {
    }
  }
}

