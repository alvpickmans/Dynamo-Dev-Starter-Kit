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


namespace $saferootprojectname$
{
  public class HelloUINodeView : INodeViewCustomization<HelloUI>
  {
    private DynamoViewModel _dynamoVM;
    private NodeView _nodeview;
    private HelloUI _model;

  public void CustomizeView(HelloUI model, NodeView nodeView)
    {
    _dynamoVM = nodeView.ViewModel.DynamoViewModel;
    _nodeview = nodeView;
    _model = model;

    var button = new Button()
    {
      Content = "Hello?",
      //Margin = new System.Windows.Thickness(10, 0, 10, 0)
    };

    button.Click += Button_Click;
    nodeView.inputGrid.Children.Add(button);
  }

  private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
  {
    MessageBox.Show("Hello there!");
  }

  public void Dispose()
    {
    }
  }
}

