using System.Collections.Generic;
using TransactionData.Core.Messeges;
using ExcelDataReader;
using TransactionData.Core.Model;

namespace TransactionData.Core
{
    public interface ITransactionProcess
    {
        void Process(string[] values);
        List<ExcelMessages> Process(List<TransactionModel> transaction);
        int ValidateExcelContent(string[] values);
        bool ValidateExcelContent(TransactionModel transaction);
        List<ExcelMessages> ValidateExcelFirstRow(IExcelDataReader excelData);
        List<ExcelMessages> ValidateExcelFirstRow(string[] value);
    }
}