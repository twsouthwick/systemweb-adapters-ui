// MIT License.

using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

#nullable disable

namespace System.Web.UI.WebControls;
/// <devdoc>
/// <para>Converts a <see cref='System.Web.UI.WebControls.FontUnit'/> to and from a specified data type.</para>
/// </devdoc>
public class FontUnitConverter : TypeConverter
{
    private StandardValuesCollection values;

    /// <devdoc>
    /// <para>Determines if the specified data type can be converted to a <see cref='System.Web.UI.WebControls.FontUnit'/>.</para>
    /// </devdoc>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) ? true : base.CanConvertFrom(context, sourceType);
    }

    /// <devdoc>
    /// <para>Converts the specified <see cref='object' qualify='true'/> into a <see cref='System.Web.UI.WebControls.FontUnit'/>.</para>
    /// </devdoc>
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value == null)
        {
            return null;
        }

        string stringValue = value as string;
        if (stringValue != null)
        {
            string textValue = stringValue.Trim();
            return textValue.Length == 0 ? FontUnit.Empty : (object)FontUnit.Parse(textValue, culture);
        }
        else
        {
            return base.ConvertFrom(context, culture, value);
        }
    }

    /// <internalonly/>
    /// <devdoc>
    ///   Returns a value indicating whether the converter can
    ///   convert to the specified destination type.
    /// </devdoc>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return (destinationType == typeof(string)) ||
            (destinationType == typeof(InstanceDescriptor))
            ? true
            : base.CanConvertTo(context, destinationType);
    }

    /// <devdoc>
    /// <para>Converts the specified <see cref='System.Web.UI.WebControls.FontUnit'/> into the specified <see cref='System.Type' qualify='true'/>. </para>
    /// </devdoc>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            return (value == null) || (((FontUnit)value).Type == FontSize.NotSet) ? string.Empty : (object)((FontUnit)value).ToString(culture);
        }
        else if ((destinationType == typeof(InstanceDescriptor)) && (value != null))
        {
            FontUnit u = (FontUnit)value;
            MemberInfo member = null;
            object[] args = null;

            if (u.IsEmpty)
            {
                member = typeof(FontUnit).GetField("Empty");
            }
            else if (u.Type != FontSize.AsUnit)
            {
                string fieldName = null;
                switch (u.Type)
                {
                    case FontSize.Smaller:
                        fieldName = "Smaller";
                        break;
                    case FontSize.Larger:
                        fieldName = "Larger";
                        break;
                    case FontSize.XXSmall:
                        fieldName = "XXSmall";
                        break;
                    case FontSize.XSmall:
                        fieldName = "XSmall";
                        break;
                    case FontSize.Small:
                        fieldName = "Small";
                        break;
                    case FontSize.Medium:
                        fieldName = "Medium";
                        break;
                    case FontSize.Large:
                        fieldName = "Large";
                        break;
                    case FontSize.XLarge:
                        fieldName = "XLarge";
                        break;
                    case FontSize.XXLarge:
                        fieldName = "XXLarge";
                        break;
                }
                Debug.Assert(fieldName != null, "Invalid FontSize type");
                if (fieldName != null)
                {
                    member = typeof(FontUnit).GetField(fieldName);
                }
            }
            else
            {
                member = typeof(FontUnit).GetConstructor(new Type[] { typeof(Unit) });
                args = new object[] { u.Unit };
            }

            Debug.Assert(member != null, "Looks like we're missing FontUnit static fields or FontUnit::ctor(Unit)");
            return member != null ? new InstanceDescriptor(member, args) : (object)null;
        }
        else
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <devdoc>
    /// <para>Returns a <see cref='System.ComponentModel.TypeConverter.StandardValuesCollection' qualify='true'/> 
    /// containing standard <see cref='System.Web.UI.WebControls.FontUnit'/> values.</para>
    /// </devdoc>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        if (values == null)
        {
            object[] namedUnits = new object[] {
                FontUnit.Smaller,
                FontUnit.Larger,
                FontUnit.XXSmall,
                FontUnit.XSmall,
                FontUnit.Small,
                FontUnit.Medium,
                FontUnit.Large,
                FontUnit.XLarge,
                FontUnit.XXLarge
            };

            values = new StandardValuesCollection(namedUnits);
        }
        return values;
    }

    /// <devdoc>
    ///    <para>Indicates whether the specified context contains exclusive standard 
    ///       values.</para>
    /// </devdoc>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
        return false;
    }

    /// <devdoc>
    ///    <para>Indicates whether the specified context contains suppurted standard 
    ///       values.</para>
    /// </devdoc>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }
}

