<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cecelia's Marketplace Database</title>
</head>
<body style="background-color: Gray">
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlHeader" runat="server" Width="100%" Height="50px" BackColor="Black" ForeColor="White">
            <asp:Table HorizontalAlign="Center" ID="tblHeader" runat="server">
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="lblHeader" runat="server" Text="Cecelia's Marketplace Database" ForeColor="White" Font-Size="XX-Large"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="pnlContent" runat="server" Width="100%" Height="100%" BackColor="LightGreen">
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Image ID="topSpace" ImageUrl="~/Images/spacer.gif" Height="160px" Width="100%" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="tblAddUser" runat="server" HorizontalAlign="Center" BorderStyle= "Solid" GridLines="Both" BorderColor="Black">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">
                        <asp:Label ID="lblEUI" runat="server" Text="Enter User Information"></asp:Label>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px"><asp:Label ID="lblUserName" runat="server" Text="Name"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="200px"><asp:TextBox ID="txtUserName" runat="server" Width="150px" /> </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px"><asp:Label ID="lblUserId" runat="server" Text="UserID"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="200px"><asp:TextBox ID="txtUserId" runat="server" Width="150px"/> </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px"><asp:Label ID="lblPassword" runat="server" Text="Password" ></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="200px"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px" /> </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px"><asp:Label ID="lblVerifyPassword" runat="server" Text="Verify Password"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="200px"><asp:TextBox ID="txtVerifyPassword" runat="server" TextMode="Password" Width="150px" /> </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="200px"><asp:Label ID="lblUserRole" runat="server" Text="User Role" Width="200px"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:DropDownList ID="ddlRole" runat="server" Width="155px" EnableViewState="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2"><asp:Label ID="lblError" runat="server" Visible="false" ForeColor = "Red"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnAddUser" runat="server" Text="Add" Width="60px" OnClick="btnAddUser_Click" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" Width="60px" OnClick="btnCancelUser_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table2" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Image ID="bottomSpace" ImageUrl="~/Images/spacer.gif" Height="160px" Width="100%" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
