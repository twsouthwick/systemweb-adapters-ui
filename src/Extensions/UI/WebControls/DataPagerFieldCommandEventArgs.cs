// MIT License.

using System.Diagnostics.CodeAnalysis;

namespace System.Web.UI.WebControls;

public class DataPagerFieldCommandEventArgs : CommandEventArgs
{
    private readonly DataPagerFieldItem _item;
    private readonly object _commandSource;

    [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", MessageId = "2#")]
    public DataPagerFieldCommandEventArgs(DataPagerFieldItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
    {
        _item = item;
        _commandSource = commandSource;
    }

    public object CommandSource
    {
        get
        {
            return _commandSource;
        }
    }

    public DataPagerFieldItem Item
    {
        get
        {
            return _item;
        }
    }
}
