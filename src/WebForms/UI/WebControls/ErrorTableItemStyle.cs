// MIT License.

using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls;

// A derived TableItemStyle class with the default ForeColor set to Color.Red instead of Color.Empty
internal sealed class ErrorTableItemStyle : TableItemStyle, ICustomTypeDescriptor
{

    public ErrorTableItemStyle() : base()
    {
        ForeColor = Color.Red;
    }

    #region ICustomTypeDesciptor implementation
    ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
        return TypeDescriptor.GetAttributes(this, true);
    }

    string ICustomTypeDescriptor.GetClassName()
    {
        return TypeDescriptor.GetClassName(this, true);
    }

    string ICustomTypeDescriptor.GetComponentName()
    {
        return TypeDescriptor.GetComponentName(this, true);
    }

    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
        return TypeDescriptor.GetConverter(this, true);
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
        return TypeDescriptor.GetDefaultEvent(this, true);
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
        return TypeDescriptor.GetDefaultProperty(this, true);
    }

    object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
    {
        return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
        return TypeDescriptor.GetEvents(this, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
    {
        return TypeDescriptor.GetEvents(this, attributes, true);
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
        return ((ICustomTypeDescriptor)this).GetProperties(null);
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
    {
        PropertyDescriptorCollection oldProperties = TypeDescriptor.GetProperties(GetType(), attributes);
        PropertyDescriptor[] newProperties = new PropertyDescriptor[oldProperties.Count];
        PropertyDescriptor foreColor = oldProperties["ForeColor"];

        for (int i = 0; i < oldProperties.Count; i++)
        {
            PropertyDescriptor property = oldProperties[i];
            if (property == foreColor)
            {
                newProperties[i] = TypeDescriptor.CreateProperty(
                    GetType(), property, new DefaultValueAttribute(typeof(Color), "Red"));
            }
            else
            {
                newProperties[i] = property;
            }
        }

        return new PropertyDescriptorCollection(newProperties, true);
    }

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
    {
        return this;
    }
    #endregion //ICustomTypeDescriptor implementation
}

