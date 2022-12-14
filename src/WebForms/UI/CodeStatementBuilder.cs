// MIT License.

namespace System.Web.UI;

using System.CodeDom;

/// <summary>
/// A ControlBuilder implementation that generates Code DOM statements
/// </summary>
public abstract class CodeStatementBuilder : ControlBuilder
{

    /// <summary>
    /// Build a CodeStatement for a generated Render method.
    /// </summary>
    public abstract CodeStatement BuildStatement(CodeArgumentReferenceExpression writerReferenceExpression);
}
