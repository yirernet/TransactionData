using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransactionData.Core;
using TransactionData.Core.Interfaces;

namespace TransactionData
{
    public partial class Transactions : Page
    {
        static readonly IIso4217DataProvider _isoProvider = new Iso4217DataProvider();
        static readonly ITransactionDataProvider _transactionDataProvider = new TransationDataProvider();
        static readonly ITransactionProcess _transactionProcess = new TransactionProcess(_isoProvider, _transactionDataProvider);
        static readonly IDataExcelReader _dataExcelReader = new DataExcelReader(_transactionProcess);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateTransactionsGridView();
            }
        }

        private void PopulateTransactionsGridView()
        {
            var transactionDataTable = _transactionDataProvider.Get();

            if (transactionDataTable.Rows.Count > 0)
            {
                gvTransactions.DataSource = transactionDataTable;
                gvTransactions.DataBind();
            }
            else
            {
                transactionDataTable.Rows.Add(transactionDataTable.NewRow());
                gvTransactions.DataSource = transactionDataTable;
                gvTransactions.DataBind();
                gvTransactions.Rows[0].Cells.Clear();
                gvTransactions.Rows[0].Cells.Add(new TableCell());
                gvTransactions.Rows[0].Cells[0].ColumnSpan = transactionDataTable.Columns.Count;
                gvTransactions.Rows[0].Cells[0].Text = "No Data Found ..!";
                gvTransactions.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        protected void gvTransactions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTransactions.EditIndex = e.NewEditIndex;
            PopulateTransactionsGridView();

            CleanErrorMessages();
        }

        protected void gvTransactions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransactions.EditIndex = -1;
            PopulateTransactionsGridView();

            CleanErrorMessages();
        }

        protected void gvTransactions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                List<string> transactionList = new List<string>();
                string transactionId = gvTransactions.DataKeys[e.RowIndex].Value.ToString();
                GridViewRow row = (GridViewRow)gvTransactions.Rows[e.RowIndex];
                Label lblID = (Label)row.FindControl("lblID");

                TextBox textAccount = (TextBox)row.Cells[1].Controls[0];
                TextBox textDescription = (TextBox)row.Cells[2].Controls[0];
                TextBox textCurrencyCode = (TextBox)row.Cells[3].Controls[0];
                TextBox textAmount = (TextBox)row.Cells[4].Controls[0];

                transactionList.Add(textAccount.Text);
                transactionList.Add(textDescription.Text);
                transactionList.Add(textCurrencyCode.Text);
                transactionList.Add(textAmount.Text);

                var errorMessages = _dataExcelReader.ValidateUpdatedData(transactionList);

                if (errorMessages.Any(m => m.IsErrored))
                {
                    throw new ArgumentException(errorMessages[0].Message.ToString());
                }

                _transactionDataProvider.Update(transactionId, transactionList);

                gvTransactions.EditIndex = -1;
                PopulateTransactionsGridView();
                lblSuccessMessage.Text = "Selected record updated successfully.";
                lblErrorMessage.Text = "";
               
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvTransactions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string transactionId = gvTransactions.DataKeys[e.RowIndex].Value.ToString();

                _transactionDataProvider.Delete(transactionId);

                PopulateTransactionsGridView();

                lblSuccessMessage.Text = "Selected record deleted successfully.";
                lblErrorMessage.Text = "";

            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        private void CleanErrorMessages()
        {
            lblSuccessMessage.Text = "";
            lblErrorMessage.Text = "";
        }

        protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PopulateTransactionsGridView();
            gvTransactions.PageIndex = e.NewPageIndex;
            gvTransactions.DataBind();

            CleanErrorMessages();
        }
    }
}