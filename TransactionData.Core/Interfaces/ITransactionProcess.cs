using System.Collections.Generic;
using TransactionData.Core.Messeges;
using ExcelDataReader;
using TransactionData.Core.Model;

namespace TransactionData.Core
{
    public interface ITransactionProcess
    {
        List<ExcelMessages> Process(List<TransactionModel> transaction);
        bool ValidateExcelContent(TransactionModel transaction);
        List<ExcelMessages> ValidateExcelFirstRow(IExcelDataReader excelData);
    }
}