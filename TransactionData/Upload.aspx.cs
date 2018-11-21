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
using System.Configuration;

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
        static readonly IDataExcelReader _dataExcelReader = new DataExcelReader(_transactionProcess);

        protected void UploadExcelDataToDatabase(object sender, EventArgs e)
        {
            try
            {
                FileUpload inputFile = fluExcel;

                // This only works with Internet Explorer
                //string filePath = inputFile.PostedFiles[0].FileName;
                //////////////////////////////////////////////////////

                string filePath = ConfigurationManager.AppSettings["ExcelFilePath"].ToString() + Path.GetFileName(inputFile.FileName);

                if (inputFile.HasFile)
                {  
                    if(inputFile.FileName.ToUpper().EndsWith(".XLSX"))
                    {
                        
                        string errorMsg = string.Empty;

                        bool hasColumnNames = true; 

                        var excelData = _dataExcelReader.GetExcelFile(filePath);

                        if (excelData != null)
                        {
                            //validate column names
                            List<ExcelMessages> errorMessages = _transactionProcess.ValidateExcelFirstRow(excelData);

                            if (errorMessages.Any(m => m.IsErrored))
                            {
                                foreach (var message in errorMessages)
                                {
                                    errorMsg += message.Message + "! <br/>";

                                }
                                MessageHandler.HandleMsg(divMessage, "message-error", errorMsg);

                                if (errorMessages.Count == 4)
                                        hasColumnNames = false;
                            }

                            //Process content
                            errorMessages = _dataExcelReader.ProcessExcelFile(excelData, hasColumnNames);
                            if (errorMessages.Count > 0)
                            {
                                    
                                foreach (var message in errorMessages)
                                {
                                    if (message.IsErrored)
                                            errorMsg += message.Message + "! <br/>";

                                }
                                   
                                MessageHandler.HandleMsg(divMessage, "message-error", errorMsg);

                                var totalUploadedRecords = errorMessages.Count(m => m.IsErrored == false);
                                MessageHandler.HandleMsg(divSuccess, "message-success", "You successfully uploaded " + totalUploadedRecords + " Transactions into the Database.");

                            }

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
                MessageHandler.HandleMsg(divMessage, "error", "Eerror!!: " + ex.Message);
            }
        }
    }
}