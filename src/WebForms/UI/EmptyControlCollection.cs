// MIT License.

namespace System.Web.UI;

/// <devdoc>
///    <para>
///       Represents a ControlCollection that is always empty.
///    </para>
/// </devdoc>
public class EmptyControlCollection : ControlCollection
{

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public EmptyControlCollection(Control owner) : base(owner)
    {
    }

    private void ThrowNotSupportedException()
    {
        throw new HttpException(SR.GetString(SR.Control_does_not_allow_children,
                                                                 Owner.GetType().ToString()));
    }

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public override void Add(Control child)
    {
        ThrowNotSupportedException();
    }

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public override void AddAt(int index, Control child)
    {
        ThrowNotSupportedException();
    }
}
