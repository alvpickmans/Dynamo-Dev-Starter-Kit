using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using Autodesk.DesignScript.Runtime;

namespace $projectname$
{
	[IsVisibleInDynamoLibrary(false)]
  public static class StringExtensions
{
    /// <summary>
    /// Template function. When you start creating your own, this can be removed
    /// </summary>
    /// <returns></returns>
    public static string ToUpperCaseCustom(this string text)
    {
        return text.ToUpperInvariant();
    }
}
}
