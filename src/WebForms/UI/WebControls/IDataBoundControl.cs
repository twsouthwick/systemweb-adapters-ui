// MIT License.

using System.Diagnostics.CodeAnalysis;

namespace System.Web.UI.WebControls;
public interface IDataBoundControl
{
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
    string DataSourceID
    {
        get;
        set;
    }

    IDataSource DataSourceObject
    {
        get;
    }

    object DataSource
    {
        get;
        set;
    }

    [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Required by ASP.NET parser.")]
    string[] DataKeyNames
    {
        get;
        set;
    }

    string DataMember
    {
        get;
        set;
    }
}
