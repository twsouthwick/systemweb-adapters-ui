// MIT License.

namespace System.Web.ModelBinding;
public class DefaultModelBinder : IModelBinder
{

    public DefaultModelBinder()
    {
        Providers = ModelBinderProviders.Providers;
    }

    public ModelBinderProviderCollection Providers
    {
        get;
        private set;
    }

    public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
    {
        ModelBindingContext newBindingContext = bindingContext;
        IModelBinder binder = Providers.GetBinder(modelBindingExecutionContext, bindingContext);
        if (binder == null && !string.IsNullOrEmpty(bindingContext.ModelName)
            && bindingContext.ModelMetadata.IsComplexType)
        {

            // fallback to empty prefix?
            newBindingContext = new ModelBindingContext(bindingContext)
            {
                ModelName = string.Empty,
                ModelMetadata = bindingContext.ModelMetadata
            };
            binder = Providers.GetBinder(modelBindingExecutionContext, newBindingContext);
        }

        if (binder != null)
        {
            bool boundSuccessfully = binder.BindModel(modelBindingExecutionContext, newBindingContext);
            if (boundSuccessfully)
            {
                // run validation
                newBindingContext.ValidationNode.Validate(modelBindingExecutionContext, parentNode: null);
                return true;
            }
        }

        return false; // something went wrong
    }
}
