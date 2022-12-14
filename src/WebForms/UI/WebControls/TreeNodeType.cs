// MIT License.

namespace System.Web.UI.WebControls;
/// <devdoc>
///     Specifies a set of tree nodes
/// </devdoc>
[Flags]
public enum TreeNodeTypes
{

    /// <devdoc>
    ///    None
    /// </devdoc>
    None = 0,

    /// <devdoc>
    ///    Root nodes only
    /// </devdoc>
    Root = 0x1,

    /// <devdoc>
    ///    Parent nodes only
    /// </devdoc>
    Parent = 0x2,

    /// <devdoc>
    ///    Leaf nodes only
    /// </devdoc>
    Leaf = 0x4,

    /// <devdoc>
    ///    All nodes
    /// </devdoc>
    All = 0x7
}
