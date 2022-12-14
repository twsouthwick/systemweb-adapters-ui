// MIT License.

namespace System.Web.ModelBinding;
/// <summary>
/// This class provides all the external things that the Model Binding System requires.
/// These include HttpContext and ModelState.
/// </summary>
public class ModelBindingExecutionContext
{

    private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
    private readonly HttpContextBase _httpContext;
    private readonly ModelStateDictionary _modelState;

    public ModelBindingExecutionContext(HttpContextBase httpContext, ModelStateDictionary modelState)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        if (modelState == null)
        {
            throw new ArgumentNullException(nameof(modelState));
        }

        _httpContext = httpContext;
        _modelState = modelState;
    }

    public virtual HttpContextBase HttpContext
    {
        get
        {
            return _httpContext;
        }
    }

    public virtual ModelStateDictionary ModelState
    {
        get
        {
            return _modelState;
        }
    }

    public virtual void PublishService<TService>(TService service)
    {
        _services[typeof(TService)] = service;
    }

    public virtual TService GetService<TService>()
    {
        return (TService)_services[typeof(TService)];
    }

    public virtual TService TryGetService<TService>()
    {
        if (_services.ContainsKey(typeof(TService)))
        {
            return (TService)_services[typeof(TService)];
        }

        return default(TService);
    }

}
