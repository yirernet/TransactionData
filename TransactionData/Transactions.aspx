<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="TransactionData.Transactions" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Transactions</h2>
    
    <asp:GridView ID="gvTransations" runat="server"></asp:GridView>

</asp:Content>
