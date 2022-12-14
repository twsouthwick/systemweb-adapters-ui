// MIT License.

namespace System.Web.UI.WebControls;
/// <devdoc>
///  Derived version of Button used within a DataControl.
/// </devdoc>
[SupportsEventValidation]
internal sealed class DataControlButton : Button
{
    readonly IPostBackContainer _container;

    internal DataControlButton(IPostBackContainer container)
    {
        _container = container;
    }

    public override bool CausesValidation
    {
        get
        {
            return false;
        }
        set
        {
            throw new NotSupportedException(SR.GetString(SR.CannotSetValidationOnDataControlButtons));
        }
    }

    public override bool UseSubmitBehavior
    {
        get
        {
            return false;
        }
        set
        {
            throw new NotSupportedException();
        }
    }

    protected sealed override PostBackOptions GetPostBackOptions()
    {
        PostBackOptions options;

        if (_container != null)
        {
            options = _container.GetPostBackOptions(this);

            if (Page != null)
            {
                options.ClientSubmit = true;
            }
        }
        else
        {
            options = base.GetPostBackOptions();
        }

        return options;
    }
}

