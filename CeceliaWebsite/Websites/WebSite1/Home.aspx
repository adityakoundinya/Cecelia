<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" EnableEventValidation="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    function highlight_row(the_element) {
        if (the_element.checked) {
            the_element.parentNode.parentNode.style.backgroundColor = 'Blue';
        } else {
            the_element.parentNode.parentNode.style.backgroundColor = '';
        }

    }
    function EnableTextboxes() {
        if (document.getElementById('rdbAny').checked || document.getElementById('rdbBoth').checked) {
            document.getElementById('<%= txtCategorySearch.ClientID %>').disabled = false;
            document.getElementById('<%= txtCompanySearch.ClientID %>').disabled = false;
            document.getElementById('<%= txtCompanySearch.ClientID %>').style.background = "white";
            document.getElementById('<%= txtCategorySearch.ClientID %>').style.background = "white";
        } else if (document.getElementById('rdbCategory').checked) {
            document.getElementById('<%= txtCategorySearch.ClientID %>').disabled = false;
            document.getElementById('<%= txtCompanySearch.ClientID %>').disabled = true;
            document.getElementById('<%= txtCompanySearch.ClientID %>').style.background = "gray";
            document.getElementById('<%= txtCategorySearch.ClientID %>').style.background = "white";
        } else if (document.getElementById('rdbCompany').checked) {
            document.getElementById('<%= txtCategorySearch.ClientID %>').disabled = true;
            document.getElementById('<%= txtCompanySearch.ClientID %>').disabled = false;
            document.getElementById('<%= txtCompanySearch.ClientID %>').style.background = "white";
            document.getElementById('<%= txtCategorySearch.ClientID %>').style.background = "gray";
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CM DB</title>
</head>
<body style="background-color: Gray;">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlMain" runat="server" Height="100%" Width="100%" HorizontalAlign="Center">
        <div>
            <asp:Panel ID="pnlHeader" runat="server" Width="100%" Height="50px" BackColor="Black" ForeColor="White">
                <asp:Table HorizontalAlign="Center" ID="tblHeader" runat="server">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="lblHeader" runat="server" Text="CM DB" ForeColor="White" Font-Size="XX-Large"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID="pnlContent" runat="server" Width="100%" Height="94%" BackColor="LightGreen" DefaultButton="btnSearch">
                <asp:Table runat="server" CellPadding="1" CellSpacing="1" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width = "25%"><asp:Label ID="lblWelcome" runat="server"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" Width = "25%">
                            <asp:Image ID="imgTop" runat="server" ImageUrl="~/Images/spacer.gif" Height="10px"/>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" Width = "25%">
                            <asp:LinkButton ID="ibAddUser" runat="server" Text="AddUser" ForeColor="Black" OnClick="ibAddUser_Click" Visible="false">
                            </asp:LinkButton>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" Width = "25%">
                            <asp:LinkButton ID="ibLogout" runat="server" Text="Logout" ForeColor="Black" OnClick="ibLogout_Click"></asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="tblAddProduct" runat="server" Width="100%" Height="125px" BorderColor="Black">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell ColumnSpan="10" HorizontalAlign = "Center">
                        Quick Add Product
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell BackColor="Gray"> 
                        Add                   
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        CF
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        SF
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        Category
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        Company Name
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        CRT
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        FAC
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        Type 1
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        Type 2
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell BackColor="Gray">
                        Flavor                    
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:Button ID="btnQuickAddProduct" runat="server" OnClick="btnQuickAddProduct_Click" Text="Add Product" OnClientClick="return window.confirm('Are you sure you want to add this product?');" Enabled="True" TabIndex="0" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:CheckBox ID="chkCF" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:CheckBox ID="chkSF" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center"><asp:TextBox ID="txtCategory" runat="server" /> </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center"><asp:TextBox ID="txtCompanyName" runat="server" /> </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:CheckBox ID="chkCT" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:CheckBox ID="chkFC" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center"><asp:TextBox ID="txtType1" runat="server" /> </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center"><asp:TextBox ID="txtType2" runat="server" /> </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center"><asp:TextBox ID="txtFlavor" runat="server" /> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table runat="server" Width="100%" Height="50px">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell ColumnSpan="2">
                                    Search Products by Company/Category
                                    </asp:TableHeaderCell></asp:TableHeaderRow>
                                <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center">
                                        <asp:RadioButton ID="rdbCompany" runat ="server" GroupName="Search" TabIndex="0" 
                                        onclick = "EnableTextboxes()"/>
                                        IsCompany
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:RadioButton ID="rdbCategory" runat="server" GroupName="Search" TabIndex="0" 
                                        onclick = "EnableTextboxes()"/>
                                        IsCategory
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:RadioButton ID="rdbAny" runat="server" GroupName="Search" TabIndex="0" 
                                        onclick = "EnableTextboxes()"/>
                                    Any
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:RadioButton ID="rdbBoth" runat="server" GroupName="Search" TabIndex="0" 
                                        onclick = "EnableTextboxes()"/>
                                        Both
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center"><asp:Label ID="lblCategory" runat="server" Text="Category:" >
                                    </asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="txtCategorySearch" runat="server" TabIndex="0" /> </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center"><asp:Label ID="lblCompany" runat="server" Text="Company:" >
                                    </asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="txtCompanySearch" runat="server" TabIndex="0" /> </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" TabIndex="0" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell><asp:TableCell HorizontalAlign="Center"><asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red">
                        </asp:Label>
                        </asp:TableCell></asp:TableRow>
                </asp:Table>
                <asp:Table runat="server" Width="100%" CellSpacing="1">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell HorizontalAlign="Left">
                            <asp:Label ID="lblsearchResults" runat="server" Text="" Visible="false"></asp:Label>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Center">
                            <asp:Label ID="lblSearchBy" runat="server" Visible="false"></asp:Label>
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
                <div style="overflow-x: auto; overflow-y: auto; height: 100%">
                    <asp:GridView ID="gridView" runat="server" Width="100%" OnRowEditing="gridView_RowEditing" OnRowCancelingEdit="gridView_RowCancelingEdit" 
                    OnRowUpdating="gridView_RowUpdating" OnRowDataBound="gridView_RowDataBound" AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging"
                     OnRowDeleting = "gridView_RowDeleting" OnSorting="gridView_ColumnSorting" OnSorted="gridView_ColumnSorted" 
                     BorderStyle=Solid  AllowSorting="True" PageSize="25">
                        <PagerSettings PageButtonCount="25" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="gridView_lnkBtn" runat="server" CommandName="Delete" OnClientClick='return confirm("Are you sure?");'
                                     Text="Delete" ForeColor = "Red"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
