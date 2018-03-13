<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PageAttributesDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Value from QueryString : <asp:Label runat="server" ID="fromQueryString"></asp:Label><br />
            Value from AppSettings : <asp:Label runat="server" ID="fromAppSettings"></asp:Label><br />
            Value from Session : <asp:Label runat="server" ID="fromSession"></asp:Label>&nbsp;&nbsp;<asp:LinkButton runat="server" ID="incrementValueFromSession" Text="Increment" OnClick="incrementValueFromSession_Click"></asp:LinkButton><br />
            Value from ViewState : <asp:Label runat="server" ID="fromViewState"></asp:Label>&nbsp;&nbsp;<asp:LinkButton runat="server" ID="incrementValueFromViewState" Text="Increment" OnClick="incrementValueFromViewState_Click"></asp:LinkButton><br />
        </div>
    </form>
</body>
</html>
