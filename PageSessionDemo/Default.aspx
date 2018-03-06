<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PageSessionDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager" EnablePageMethods="true"></asp:ScriptManager>
        <div>
            Session Value: <asp:TextBox runat="server" ID="sessionValue"></asp:TextBox>
            <asp:LinkButton runat="server" ID="incrementLink" Text="Increment" OnClick="incrementLink_Click"></asp:LinkButton>
        </div>
        <br />
        <div>
            Full Session List<br />
            <asp:Label ID="fullSessionList" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
