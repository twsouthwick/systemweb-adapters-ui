// MIT License.

namespace System.Web.UI;
/// <devdoc>
/// Implemented by the designer host to participate in user control type resolution.
/// </devdoc>
public interface IUserControlTypeResolutionService
{

    Type GetType(string tagPrefix, string tagName);
}
