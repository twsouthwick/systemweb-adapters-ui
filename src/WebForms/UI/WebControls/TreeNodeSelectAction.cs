// MIT License.

namespace System.Web.UI.WebControls;

/// <devdoc>
///    Specifies the action the TreeView takes when a node is selected
/// </devdoc>
public enum TreeNodeSelectAction
{

    /// <devdoc>
    ///    Select the node
    /// </devdoc>
    Select = 0,

    /// <devdoc>
    ///    Expand the node
    /// </devdoc>
    Expand = 1,

    /// <devdoc>
    ///    Select and expand the node
    /// </devdoc>
    SelectExpand = 2,

    /// <devdoc>
    ///    Do nothing when clicking on a node
    /// </devdoc>
    None = 3
}
