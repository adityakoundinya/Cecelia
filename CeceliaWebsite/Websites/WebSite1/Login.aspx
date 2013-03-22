<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CM DB</title>
</head>
<body style="background-color: Gray">
    <form id="form1" runat="server">
        <asp:Panel ID="pnlMain" runat="server" Height="100%" Width="100%" HorizontalAlign="Center">
    <div>
        <asp:Panel ID="pnlHeader" runat="server" Width="100%" Height="100%" BackColor="Black" ForeColor="White">
            <asp:Table HorizontalAlign="Center" ID="tblHeader" runat="server">
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="lblHeader" runat="server" Text="CM DB" ForeColor="White" Font-Size="XX-Large"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="pnlContent" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" BackColor="LightGreen" DefaultButton="btnLogin">
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Image ID="topSpace" ImageUrl="~/Images/spacer.gif" Height="100%" Width="100%" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table runat="server" HorizontalAlign = "Center">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign = "Center">
                        <asp:Table ID="Table1" runat="server" BackColor="Gray" Width="100%" Height="100%" BorderStyle="Inset" HorizontalAlign="Center"
                         BorderColor="Black">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table runat="server">
                                        <asp:TableRow Height="25px">
                                            <asp:TableCell HorizontalAlign="Left"><asp:Label ID="lblUserName" runat="server" Text="UserName" Width="100px" 
                                            Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Right"><asp:TextBox ID="txtUserName" runat="server" Width="150px" TabIndex = "1" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Height="25px">
                                            <asp:TableCell HorizontalAlign="Left"><asp:Label ID="lblPassword" runat="server" Text="Password" Width="100px" 
                                            Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Right"><asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" Width="150px"
                                             TabIndex = "2" /></asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                            
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Table ID="Table3" runat="server">
                                        <asp:TableRow Height="25px">
                                            <asp:TableCell HorizontalAlign="Right" Width="150px">
                                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Width="60px" TabIndex="3" />
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="150px">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Width="60px" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table4" runat="server">
                <asp:TableRow Height="25px" HorizontalAlign="Center">
                    <asp:TableCell HorizontalAlign="Center"><asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table2" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Image ID="bottomSpace" ImageUrl="~/Images/spacer.gif" Height="100%" Width="100%" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
    </div>
        <asp:Label ID="lblVersion" runat="server"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>
