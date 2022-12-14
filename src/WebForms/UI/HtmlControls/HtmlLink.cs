// MIT License.

using System.ComponentModel;

namespace System.Web.UI.HtmlControls;
[
ControlBuilderAttribute(typeof(HtmlEmptyTagControlBuilder))
]
public class HtmlLink : HtmlControl
{

    public HtmlLink() : base("link")
    {
    }

    [
    WebCategory("Action"),
    DefaultValue(""),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
    UrlProperty(),
    ]
    public virtual string Href
    {
        get
        {
            string s = Attributes["href"];
            return ((s != null) ? s : String.Empty);
        }
        set
        {
            Attributes["href"] = MapStringAttributeToString(value);
        }
    }

    protected override void RenderAttributes(HtmlTextWriter writer)
    {
        // Resolve the client href based before rendering the attribute.
        if (!String.IsNullOrEmpty(Href))
        {
            Attributes["href"] = ResolveClientUrl(Href);
        }

        base.RenderAttributes(writer);
    }

    protected internal override void Render(HtmlTextWriter writer)
    {
        writer.WriteBeginTag(TagName);
        RenderAttributes(writer);
        writer.Write(HtmlTextWriter.SelfClosingTagEnd);
    }
}
