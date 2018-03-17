<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PageAttributesDemo.Default" MasterPageFile="~/Master.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="content">
Value from QueryString : <asp:Label runat="server" ID="fromQueryString"></asp:Label><br />
Value from AppSettings : <asp:Label runat="server" ID="fromAppSettings"></asp:Label><br />
Value from Session : <asp:Label runat="server" ID="fromSession"></asp:Label>&nbsp;&nbsp;<asp:LinkButton runat="server" ID="incrementValueFromSession" Text="Increment" OnClick="incrementValueFromSession_Click"></asp:LinkButton><br />
Value from ViewState : <asp:Label runat="server" ID="fromViewState"></asp:Label>&nbsp;&nbsp;<asp:LinkButton runat="server" ID="incrementValueFromViewState" Text="Increment" OnClick="incrementValueFromViewState_Click"></asp:LinkButton><br />
</asp:Content>
