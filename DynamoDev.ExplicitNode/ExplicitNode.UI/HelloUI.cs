using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
/* dynamo directives */
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;
using ProtoCore.AST.AssociativeAST;
using $saferootprojectname$;

namespace $saferootprojectname$.UI
{
  [NodeName("HelloUI")]
  [NodeDescription("Sample Explicit Node")]
  [NodeCategory("$saferootprojectname$.Sample Nodes")]
  [IsDesignScriptCompatible]
public class HelloUI : NodeModel
{
    
  /// <summary>
  /// Json Constructor for nodes on Dynamo 2.0 and above.
  /// </summary>
  /// <param name="inPorts"></param>
  /// <param name="outPorts"></param>
  [JsonConstructor]
  private HelloUI(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
  {

  }

  /// <summary>
  /// Constructor for nodes.
  /// </summary>
  public HelloUI()
  {
      RegisterAllPorts();
  }
}
}
