using AutoMapper;
using System.Collections.Generic;
using TransactionData.Core.Interfaces;
using TransactionData.Core.Messeges;
using System.Data;
using TransactionData.DAL;
using ExcelDataReader;
using TransactionData.Core.Model;

namespace TransactionData.Core
{
    public class TransactionProcess : ITransactionProcess
    {
        private readonly IIso4217DataProvider _currencyProvider;
        private readonly ITransactionDataProvider _transactionDataProvider;

        //Constructor
        public TransactionProcess(IIso4217DataProvider currencyProvider, ITransactionDataProvider transactionDataProvider)
        {
            _currencyProvider = currencyProvider;
            _transactionDataProvider = transactionDataProvider;
          
        }

        public List<ExcelMessages> ValidateExcelFirstRow(IExcelDataReader excelData)
        {
            DataSet excelDataSet = excelData.AsDataSet();

            string errorKey = "FileValidation";
            var errorMessages = new List<ExcelMessages>();
            if (excelData.RowCount == 0)
            {
                errorMessages.Add(
                    new ExcelMessages()
                    {
                        Key = errorKey,
                        Message = $"The file doesn't have any rows.",
                        IsErrored = true
                    }
                );
            }

            if (excelDataSet.Tables[0].Rows[0].ItemArray[0].ToString().ToUpper() != "ACCOUNT")
            {
                errorMessages.Add(
                    new ExcelMessages()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the first column should be Account",
                        IsErrored = true
                    }
                );
            }

            if (excelDataSet.Tables[0].Rows[0].ItemArray[1].ToString().ToUpper() != "DESCRIPTION")
            {
                errorMessages.Add(
                    new ExcelMessages()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the second column should be Description",
                        IsErrored = true
                    }
                );

            }

            if (excelDataSet.Tables[0].Rows[0].ItemArray[2].ToString().ToUpper() != "CURRENCY CODE")
            {
                errorMessages.Add(
                    new ExcelMessages()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the third column should be Currency Code",
                        IsErrored = true
                    }
                );
            }

            if (excelDataSet.Tables[0].Rows[0].ItemArray[3].ToString().ToUpper() != "AMOUNT")
            {
                errorMessages.Add(
                    new ExcelMessages()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the last column should be Amount",
                        IsErrored = true
                    }
                );
            }

            return errorMessages;
        }

        public bool ValidateExcelContent(TransactionModel transaction)
        {
            if (string.IsNullOrEmpty(transaction.Account)) return false;
            if (string.IsNullOrEmpty(transaction.Description)) return false;
            if (string.IsNullOrEmpty(transaction.Amount.ToString())) return false;

            decimal output = 0;
            if (!decimal.TryParse(transaction.Amount.ToString(), out output)) return false;

            var validate = _currencyProvider.ValidateCode(transaction.CurrencyCode);
            return validate;
        }

        public List<ExcelMessages> Process(List<TransactionModel> transactions)
        {
            var processMessages = new List<ExcelMessages>();
            foreach (var transaction in transactions)
            {
                if (!ValidateExcelContent(transaction))
                {
                    processMessages.Add(
                       new ExcelMessages()
                       {
                           Key = "ProcessValidation",
                           Message =
                             $"Cound not process transaction ( Account: {transaction.Account} , Desciption: {transaction.Description}, Currency: {transaction.CurrencyCode}, Amount: {transaction.Amount} )",
                           IsErrored = true
                       });
                }
                else
                {
                    int transactionId = _transactionDataProvider.Save(transaction);

                    if (transactionId > 0)
                    {
                        processMessages.Add(
                        new ExcelMessages()
                        {
                            Key = "ProcessValidation",
                            Message =
                              "Transaction Saved into Database",
                            IsErrored = false
                        });

                    }
                    else
                    {
                        processMessages.Add(
                        new ExcelMessages()
                        {
                            Key = "ProcessValidation",
                            Message =
                              $"Error in saving transaction Account: {transaction.Account} , Desciption: {transaction.Description}, Currency: {transaction.CurrencyCode}, Amount: {transaction.Amount} )",
                            IsErrored = true
                        });
                    }
                }
            }

            return processMessages;
        }

        
    }
}
