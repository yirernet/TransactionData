<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TransactionData._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
<section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Home page</h1>
            </hgroup>
            <p>
                This web application has been designed to process the account transaction data to calculate tax figures for a client tax return.
                The transaction data has 4 columns: <mark>Account, Description, Currency Code and Amount</mark> and each individual file could contain up to 100k rows..
                You can choose between displaying the list of transactions or upload a Excel file to import and insert into the database.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>There is two features:</h3>
    <ol class="round">
        <li class="one">
            <h5>Transactions</h5>
            It shows you all single data transaction stored inside the database with their corresponding id
            <a href="/Transactions">Click here…</a>
        </li>
        <li class="two">
            <h5>Upload file</h5>
            You can upload a Excel file with specific fields to import and insert into the database
            <a href="/Upload">Click here…</a>
        </li>
    </ol>

</asp:Content>
