using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace $safeprojectname$
{
	/// <summary>
  /// Hello World basic sample
  /// </summary>
  public static class HelloDynamo
  {
  /// <summary>
  /// Prints a friendly greeting
  /// </summary>
  /// <param name="name">Name of the person to greet</param>
  /// <returns>A greeting</returns>
  public static string SayHello(string name)
      {
          return String.Format("Hello {0}!", name);
      }
  }
}
