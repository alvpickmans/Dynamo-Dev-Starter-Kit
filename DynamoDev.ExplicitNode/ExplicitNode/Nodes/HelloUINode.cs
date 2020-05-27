using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
/* dynamo directives */
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;
using ProtoCore.AST.AssociativeAST;
using $projectname$.Functions;

namespace $projectname$.Nodes
{
  [NodeName("HelloUI")]
  [NodeDescription("Sample Explicit Node")]
  [NodeCategory("$projectname$.Sample Nodes")]
  [IsDesignScriptCompatible]
public class HelloUINode : NodeModel
{
    
  /// <summary>
  /// Json Constructor for nodes on Dynamo 2.0 and above.
  /// </summary>
  /// <param name="inPorts"></param>
  /// <param name="outPorts"></param>
  [JsonConstructor]
  private HelloUINode(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
  {

  }

  /// <summary>
  /// Constructor for nodes.
  /// </summary>
  public HelloUINode()
  {
      RegisterAllPorts();
  }
}
}
