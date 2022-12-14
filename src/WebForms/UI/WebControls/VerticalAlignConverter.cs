// MIT License.

using System.ComponentModel;
using System.Globalization;

// 

#nullable disable

namespace System.Web.UI.WebControls;
internal sealed class VerticalAlignConverter : EnumConverter
{

    static readonly string[] stringValues = new string[(int)VerticalAlign.Bottom + 1];

    static VerticalAlignConverter()
    {
        stringValues[(int)VerticalAlign.NotSet] = "NotSet";
        stringValues[(int)VerticalAlign.Top] = "Top";
        stringValues[(int)VerticalAlign.Middle] = "Middle";
        stringValues[(int)VerticalAlign.Bottom] = "Bottom";
    }

    // this constructor needs to be public despite the fact that it's in an internal
    // class so it can be created by Activator.CreateInstance.
    public VerticalAlignConverter() : base(typeof(VerticalAlign)) { }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) ? true : base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value == null)
        {
            return null;
        }

        if (value is string)
        {
            string textValue = ((string)value).Trim();
            if (textValue.Length == 0)
            {
                return VerticalAlign.NotSet;
            }

            switch (textValue)
            {
                case "NotSet":
                    return VerticalAlign.NotSet;
                case "Top":
                    return VerticalAlign.Top;
                case "Middle":
                    return VerticalAlign.Middle;
                case "Bottom":
                    return VerticalAlign.Bottom;
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) ? true : base.CanConvertTo(context, sourceType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        return destinationType == typeof(string) && ((int)value <= (int)VerticalAlign.Bottom)
            ? stringValues[(int)value]
            : base.ConvertTo(context, culture, value, destinationType);
    }
}

