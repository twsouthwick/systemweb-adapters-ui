<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" Inherits="SystemWebUISample.Pages.MyPage" AutoEventWireup="true" CodeBehind="About.aspx.cs"  %>

<h1>Edit3</h1>

<br />

<form id="frm" runat="server">
    <asp:Label ID="txt" Text="First Name" runat="server" />
    <asp:TextBox id="txt" runat="server" Value="Leave it alone" />

    <!-- HTML comment test -->
    <%-- ASP comment test --%>

    <asp:RequiredFieldValidator runat="server" ControlToValidate="Name" Display="Dynamic"
            CssClass="field-validation-valid text-danger" ErrorMessage="The Name field is required." />

    <asp:Button id="button1" value="Go" runat="server" Text="Click Me" OnClick="Button1_Click" />
</form>