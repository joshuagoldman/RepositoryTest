<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationBase.aspx.cs" Inherits="JoshuaWebAPI.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
            margin-left: 240px;
        }
        .auto-style2 {
            height: 485px;
        }
        .auto-style3 {
            margin-left: 13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style2">
        <div>
            <p class="auto-style1">
                <strong>Information Base</strong></p>
        </div>
        <p>
            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem>Store New Category </asp:ListItem>
                <asp:ListItem>Store New Subject</asp:ListItem>
                <asp:ListItem>Obtain</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            &nbsp;</p>
        <p id="Labels" class="auto-style3">
            Categories&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Category&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Subject&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fact&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Value&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </p>
        <asp:TextBox ID="Categories" runat="server" OnTextChanged="Categories_TextChanged" Width="92px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Category" runat="server" Height="17px" OnTextChanged="Category_TextChanged" Width="92px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Subject" runat="server" OnTextChanged="Subject_TextChanged" Width="92px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Fact" runat="server" OnTextChanged="Fact_TextChanged" Width="92px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Value" runat="server" OnTextChanged="Value_TextChanged" Width="92px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Result<br />
        <br />
        <asp:TextBox ID="Result" runat="server" OnTextChanged="Result_TextChanged"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="ActionButton" runat="server" OnClick="ActionButton_Click" Text="Store" />
    </form>
</body>
</html>
