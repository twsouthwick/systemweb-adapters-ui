// MIT License.

namespace System.Web.ModelBinding;
[Serializable]
public class ModelState
{

    private readonly ModelErrorCollection _errors = new ModelErrorCollection();

    public ValueProviderResult Value
    {
        get;
        set;
    }

    public ModelErrorCollection Errors
    {
        get
        {
            return _errors;
        }
    }
}
