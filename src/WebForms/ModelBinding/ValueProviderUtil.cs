// MIT License.

namespace System.Web.ModelBinding;
internal static class ValueProviderUtil
{

    public static string CreateSubPropertyName(string prefix, string propertyName)
    {
        if (String.IsNullOrEmpty(prefix))
        {
            return propertyName;
        }
        else if (String.IsNullOrEmpty(propertyName))
        {
            return prefix;
        }
        else
        {
            return prefix + "." + propertyName;
        }
    }

}
