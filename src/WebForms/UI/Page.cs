// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI.Features;
using System.Web.UI.HtmlControls;

// TODO: Remove once implemented
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CA1823 // Avoid unused private fields

namespace System.Web.UI;

public class Page : TemplateControl, IHttpAsyncHandler
{
    internal const string systemPostFieldPrefix = "__";

    /// <internalonly/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal const string postEventSourceID = systemPostFieldPrefix + "EVENTTARGET";

    private const string lastFocusID = systemPostFieldPrefix + "LASTFOCUS";
    private const string _scrollPositionXID = systemPostFieldPrefix + "SCROLLPOSITIONX";
    private const string _scrollPositionYID = systemPostFieldPrefix + "SCROLLPOSITIONY";

    /// <internalonly/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal const string postEventArgumentID = systemPostFieldPrefix + "EVENTARGUMENT";

    internal const string ViewStateFieldPrefixID = systemPostFieldPrefix + "VIEWSTATE";
    internal const string ViewStateFieldCountID = ViewStateFieldPrefixID + "FIELDCOUNT";
    internal const string ViewStateGeneratorFieldID = ViewStateFieldPrefixID + "GENERATOR";
    internal const string ViewStateEncryptionID = systemPostFieldPrefix + "VIEWSTATEENCRYPTED";
    internal const string EventValidationPrefixID = systemPostFieldPrefix + "EVENTVALIDATION";

    private ClientScriptManager? _clientScriptManager;

    public Page()
    {
    }

    bool IHttpHandler.IsReusable => false;

    IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object? extraData)
        => TaskAsyncHelper.BeginTask(() => ProcessAsync(context), cb, extraData);

    void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
        => TaskAsyncHelper.EndTask(result);

    void IHttpHandler.ProcessRequest(HttpContext context)
        => throw new InvalidOperationException();

    internal bool EnableEventValidation => false;

    public bool IsCallback { get; private set; }

    internal bool IsInOnFormRender => Features.GetRequired<IFormWriterFeature>().IsRendering;

    internal bool ContainsCrossPagePost { get; set; }

    internal Task ProcessAsync(HttpContext context)
    {
        context.Response.ContentType = "text/html";

        if (Features.Get<HttpContext>() is { })
        {
            throw new InvalidOperationException("Page has already been processed.");
        }

        var events = Features.Get<IPageEvents>()!;

        events.OnPreInit();

        InitializeComponents();

        if (Features.Get<IViewStateManager>() is { } viewState)
        {
            viewState.RefreshControls();
        }

        Features.Set(context);

        events.OnPageLoad();

        using var writer = new HtmlTextWriter(context.Response.Output);

        Render(writer);

        return Task.CompletedTask;
    }

    public HtmlForm? Form => Features.Get<IFormWriterFeature>()?.Form;

    public ClientScriptManager ClientScript => _clientScriptManager ??= new ClientScriptManager(this);

    internal IScriptManager ScriptManager => Features.GetRequired<IScriptManager>();

    public bool IsPostBackEventControlRegistered { get; internal set; }

    internal Control? AutoPostBackControl { get; set; }

    protected virtual void InitializeComponents()
    {
    }

    private List<Control>? _enabledControls;

    private List<Control> EnabledControls => _enabledControls ??= new();

    internal void RegisterEnabledControl(Control control)
        => EnabledControls.Add(control);

    internal void RegisterWebFormsScript()
    {
    }

    internal void RegisterPostBackScript()
    {
        ClientScript.RegisterHiddenField(postEventSourceID, string.Empty);
        ClientScript.RegisterHiddenField(postEventArgumentID, string.Empty);
    }

    internal void RegisterFocusScript()
    {
    }

    internal void Validate(string validationGroup)
    {
    }

    internal NameValueCollection RequestValueCollection => Context.Request.Params;

    internal IStateFormatter2 CreateStateFormatter() => Features.GetRequired<IStateFormatter2>();

    internal bool ShouldSuppressMacValidationException(Exception ex) => true;

    internal void VerifyRenderingInServerForm(Control control)
    {
        if (!Features.GetRequired<IFormWriterFeature>().IsRendering)
        {
            throw new InvalidOperationException("Must be in a form");
        }
    }

    internal IReadOnlyCollection<object> GetValidators(string validationGroup)
        => Array.Empty<object>();

    internal bool GetDesignModeInternal() => false;

    internal void PushDataBindingContext(object dataItem)
    {
    }

    internal void PopDataBindingContext()
    {
        throw new NotImplementedException();
    }

    internal void ApplyControlSkin(Control control)
    {
        throw new NotImplementedException();
    }

    internal bool ApplyControlStyleSheet(Control control)
    {
        throw new NotImplementedException();
    }

    internal Task GetWaitForPreviousStepCompletionAwaitable() => Task.CompletedTask;

    internal void RegisterRequiresClearChildControlState(Control control)
    {
        throw new NotImplementedException();
    }

    internal void SetFocus(Control control)
    {
        throw new NotImplementedException();
    }

    internal bool ShouldLoadControlState(Control control)
    {
        throw new NotImplementedException();
    }

    internal bool RequiresControlState(Control control)
    {
        throw new NotImplementedException();
    }

    internal void UnregisterRequiresControlState(Control control)
    {
        throw new NotImplementedException();
    }

    internal void RegisterRequiresControlState(Control control)
    {
        throw new NotImplementedException();
    }

    internal bool ClientSupportsJavaScript => true;

    internal bool SupportsCallback => true;

    internal HttpRequest? RequestInternal => Context.Request;

    public string? ClientOnSubmitEvent { get; internal set; }

    internal string ClientState => Features.GetRequired<IViewStateManager>().ClientState;

    internal string RequestViewStateString => Features.GetRequired<IViewStateManager>().OriginalState;

    public bool RenderDisabledControlsScript { get; internal set; }

    public int MaxPageStateFieldLength { get; internal set; } = 1000;
    public bool ContainsTheme { get; internal set; }
    public bool IsPostBack { get; internal set; }
}
