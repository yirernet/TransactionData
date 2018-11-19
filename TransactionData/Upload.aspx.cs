using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransactionData.Core;
using TransactionData.Core.Interfaces;
using TransactionData.Core.Messeges;
using TransactionData.Handler;

namespace TransactionData
{
    public partial class Upload : Page
    {
        protected static void Page_Load(object sender, EventArgs e)
        {

        }

        static readonly IIso4217DataProvider _isoProvider = new Iso4217DataProvider();
        static readonly ITransactionDataProvider _transactionDataProvider = new TransationDataProvider();
        static readonly ITransactionProcess _transactionProcess = new TransactionProcess(_isoProvider, _transactionDataProvider);


        protected void UploadExcelDataToDatabase(object sender, EventArgs e)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            try
            {
                string errors = "";
                int i = 0, j = 0;
                FileUpload inputFile = fluExcel;
                string filePath = inputFile.PostedFiles[0].FileName;
                bool IsFirstRowDone = false;

                if (inputFile.HasFile)
                {
                    if (inputFile.FileName.ToUpper().EndsWith(".XLSX") || inputFile.FileName.ToUpper().EndsWith(".CSV"))
                    {
                        // Create a datatable to bulk insert into the DB
                        //DataTable csvData = DataAccess.InitDataTable();
                        
                        // First read the file to check format
                        using (StreamReader reader = new StreamReader(File.OpenRead(filePath)))
                        {
                            // For each line of the file
                            while (!reader.EndOfStream)
                            {
                                i++;
                                var line = reader.ReadLine();
                                var values = line.Split(',');

                                if (!IsFirstRowDone)
                                {
                                    //Validate first row for Account, Description, Currency Code and Amount
                                    List<ExcelMessages> errorMessage = _transactionProcess.ValidateExcelFirstRow(values.ToArray());
                                
                                    if (errorMessage.Count > 0)
                                    {
                                        MessageHandler.HandleMsg(divMessage, "message-error", errorMessage[0].Message);
                                        i = 0;
                                        break;
                                    }
                                    IsFirstRowDone = true;        
                                }

                                if (i > 1)
                                {
                                    // Test the correctness of the whole line and returns a code error
                                    int errorCode = _transactionProcess.ValidateExcelContent(values.ToArray());

                                    // If no error, create new row to insert
                                    if (errorCode == 0)
                                    {
                                        _transactionProcess.Process(values.ToArray());

                                    }
                                    // Else display issue on website
                                    else
                                    {
                                        j++;
                                        errors += "Error on row " + i + ": " + ErrorCode.ErrorDetails(errorCode) + "! <br/>";
                                    }
                                }
                            }

                        }

                        if (i > 0)
                        {
                            MessageHandler.HandleMsg(divSuccess, "message-success", "Results: " + (i - j) + "/" + i + " rows correctly imported!");
                        }
                        if (j > 0)
                        {
                            MessageHandler.HandleMsg(divMessage, "message-error", errors);
                        }
                    }
                    else
                    {
                        MessageHandler.HandleMsg(divMessage, "error", "Please select a valid Excel file!!");
                    }
                }
            }
            catch (Exception ex)
            {
               

            }
        }
    }
}