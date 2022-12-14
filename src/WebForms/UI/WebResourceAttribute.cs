// MIT License.

using System.Diagnostics.CodeAnalysis;

namespace System.Web.UI;
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class WebResourceAttribute : Attribute
{

    private readonly string _contentType;
    private bool _performSubstitution;
    private readonly string _webResource;
    private string _cdnPath;
    private string _cdnPathSecureConnection;
    private bool _cdnSupportsSecureConnection;

    internal const string _microsoftCdnBasePath = "http://ajax.aspnetcdn.com/ajax/4.6/1/";

    public WebResourceAttribute(string webResource, string contentType)
    {
        if (String.IsNullOrEmpty(webResource))
        {
            throw new ArgumentOutOfRangeException(nameof(webResource));
        }

        if (String.IsNullOrEmpty(contentType))
        {
            throw new ArgumentOutOfRangeException(nameof(contentType));
        }

        _contentType = contentType;
        _webResource = webResource;
        _performSubstitution = false;
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cdn", Justification = "Stands for Content Delivery Network.")]
    public string CdnPath
    {
        get
        {
            return _cdnPath ?? String.Empty;
        }
        set
        {
            _cdnPath = value;
        }
    }

    public string LoadSuccessExpression
    {
        get;
        set;
    }

    internal string CdnPathSecureConnection
    {
        get
        {
            if (_cdnPathSecureConnection == null)
            {
                string cdnPath = CdnPath;
                if (String.IsNullOrEmpty(cdnPath) || !CdnSupportsSecureConnection || !cdnPath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                {
                    cdnPath = String.Empty;
                }
                else
                {
                    // convert http to https
                    cdnPath = string.Concat("https", cdnPath.AsSpan(4));
                }
                _cdnPathSecureConnection = cdnPath;
            }
            return _cdnPathSecureConnection;
        }
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cdn", Justification = "Stands for Content Delivery Network.")]
    public bool CdnSupportsSecureConnection
    {
        get
        {
            return _cdnSupportsSecureConnection;
        }
        set
        {
            _cdnSupportsSecureConnection = value;
        }
    }

    public string ContentType
    {
        get
        {
            return _contentType;
        }
    }

    public bool PerformSubstitution
    {
        get
        {
            return _performSubstitution;
        }
        set
        {
            _performSubstitution = value;
        }
    }

    public string WebResource
    {
        get
        {
            return _webResource;
        }
    }
}

