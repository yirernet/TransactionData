<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="TransactionData.Upload" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h2>Upload a CSV file containing transactions data.</h2>
    </hgroup>

    <section class="contact">
        <form id="form1" runat="server">
            <div>
                <div>
                    <h3>
                        Import CSV File for Inserting into database

                    </h3>
                </div>
                <div id="divMessage" runat="server" style="display: none;" ></div>
                <div id="divSuccess" runat="server" style="display: none;"></div>
                <asp:FileUpload ID="fluExcel" runat="server" />

                <asp:RequiredFieldValidator ID="rfvExcel" runat="server" ErrorMessage="Please Select a file first"
                    ValidationGroup="validate" Display="Dynamic" ControlToValidate="fluExcel">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revExcel" runat="server" ErrorMessage="Upload Excel File"
                    ValidationGroup="validate" Display="Dynamic" ValidationExpression="^.*\.(xlsx|XLSX)$"
                    ControlToValidate="fluExcel">
                </asp:RegularExpressionValidator>
                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadExcelDataToDatabase"
                    CausesValidation="true" ValidationGroup="validate" />
            </div>
        </form>
    </section>
</asp:Content>
