<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="TransactionData.Transactions" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>Transactions</h2>
    <br />
        <asp:GridView ID="gvTransactions" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
 HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True"  AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="gvTransactions_RowCancelingEdit" OnRowDeleting="gvTransactions_RowDeleting" OnRowEditing="gvTransactions_RowEditing" OnRowUpdating="gvTransactions_RowUpdating" OnPageIndexChanging="gvTransactions_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Account" HeaderText="Account" SortExpression="Account" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                 <asp:CommandField ShowEditButton="true" >  
                 <ControlStyle ForeColor="#d10011" />
                </asp:CommandField>
                 <asp:CommandField ShowDeleteButton="true" > 
                <ControlStyle ForeColor="#d10011" />
                </asp:CommandField>
            </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
 </asp:GridView>
        <br />
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" CssClass="message-success"  />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" CssClass="message-error" />

  </form>

</asp:Content>
