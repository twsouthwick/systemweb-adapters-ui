// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Web.UI;

public class Page : TemplateControl, IHttpAsyncHandler
{
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

    internal Task ProcessAsync(HttpContext context)
    {
        if (Features.Get<HttpContext>() is { })
        {
            throw new InvalidOperationException("Page has already been processed.");
        }

        var events = Features.Get<IPageEvents>()!;

        Features.Set(context);

        events.OnPageLoad(this);

        using var writer = new HtmlTextWriter(context.Response.Output);

        Render(writer);

        return Task.CompletedTask;
    }

    private const string HiddenClassName = "aspNetHidden";

    internal static void BeginFormRender(HtmlTextWriter writer, string? formUniqueID)
    {
        writer.WriteBeginTag("div");
        writer.WriteAttribute("class", HiddenClassName);
        writer.WriteEndTag("div");
    }
}