// MIT License.

namespace System.Web.UI.WebControls;

public interface IPostBackContainer
{

    /// <summary>
    /// Enables controls to obtain client-side script options that will cause
    /// (when invoked) a server post-back to the form on a button click.
    /// </summary>
    PostBackOptions GetPostBackOptions(IButtonControl buttonControl);
}
