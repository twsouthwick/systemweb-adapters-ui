// MIT License.

namespace System.Web.UI.HtmlControls;
/// <devdoc>
/// Used as ControlBuilder for controls that do not have a body or end
/// tag, for example, INPUT and IMG.
/// </devdoc>

public sealed class HtmlEmptyTagControlBuilder : ControlBuilder
{

    // <devdoc>
    // Indicate that the control does not have a body or end tag.
    // </devdoc>
    public override bool HasBody()
    {
        return false;
    }
}
