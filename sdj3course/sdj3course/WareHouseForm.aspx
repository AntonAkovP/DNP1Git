<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WareHouseForm.aspx.cs" Inherits="sdj3course.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ListBox ID="inventoryList" runat="server" Height="251px" style="margin-top: 0px" Width="231px"></asp:ListBox>
        <asp:ListBox ID="orderList" runat="server" Height="251px" style="margin-left: 44px; margin-top: 33px" Width="206px"></asp:ListBox>
    
    </div>
        <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight" Height="116px" HorizontalAlign="Left" Width="478px">
            <br />
            <asp:Button ID="addB" runat="server" Text="Add" OnClick="addB_Click" style="margin-left: 76px; margin-top: 0px" Width="68px" />
            <asp:Button ID="removeB" runat="server" OnClick="removeB_Click" style="margin-left: 200px" Text="Remove" Width="68px" />
            <br />
            <br />
            <asp:Button ID="refreshB" runat="server" OnClick="refreshB_Click" style="margin-left: 76px" Text="Refresh" Width="68px" />
            <asp:Button ID="orderB" runat="server" style="margin-left: 200px" Text="Order" Width="68px" OnClick="orderB_Click" />
        </asp:Panel>
    </form>
</body>
</html>
