using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader.Core;
using ExcelDataReader;
using TransactionData.Core.Interfaces;
using TransactionData.Core.Messeges;
using TransactionData.Core.Model;

namespace TransactionData.Core
{
    public class DataExcelReader : IDataExcelReader
    {
        private readonly ITransactionProcess _transactionProcess;

        public DataExcelReader(ITransactionProcess transactionProcess)
        {
            _transactionProcess = transactionProcess;
        }

        public IExcelDataReader GetExcelFile(string file)
        {
            try
            {
                IExcelDataReader excelReader;

                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
               
                excelReader = ExcelReaderFactory.CreateReader(stream);   
                
                return excelReader;
            }
            catch (Exception ex)
            {
                // log the exception
                throw ex;
            }
        }

        public List<ExcelMessages> ProcessExcelFile(IExcelDataReader excelData, bool hasColumnNames)
        {
            var errorMessages = new List<ExcelMessages>();

            DataSet excelDataSet = excelData.AsDataSet();

            var transactions = new List<TransactionModel>();

            //ignore first transaction as that it caption  (j = 1 instead of j = 0)
            int j = (hasColumnNames) ? 1 : 0;
                       
            for ( ; j < excelDataSet.Tables[0].Rows.Count; j++)
            {
                transactions.Add(new TransactionModel
                {
                    Account = (excelDataSet.Tables[0].Rows[j].ItemArray[0].ToString() == null)? string.Empty : excelDataSet.Tables[0].Rows[j].ItemArray[0].ToString(),
                    Description = (excelDataSet.Tables[0].Rows[j].ItemArray[1].ToString() == null)? string.Empty : excelDataSet.Tables[0].Rows[j].ItemArray[1].ToString(),
                    CurrencyCode = (excelDataSet.Tables[0].Rows[j].ItemArray[2].ToString() == null)? string.Empty : excelDataSet.Tables[0].Rows[j].ItemArray[2].ToString(),
                    Amount = (excelDataSet.Tables[0].Rows[j].ItemArray[3] == null)? string.Empty : excelDataSet.Tables[0].Rows[j].ItemArray[3].ToString()
                });

            }

            errorMessages = _transactionProcess.Process(transactions);

            return errorMessages;
        }

    }


}
