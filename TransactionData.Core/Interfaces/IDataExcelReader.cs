using ExcelDataReader;
using System.Collections.Generic;
using TransactionData.Core.Messeges;

namespace TransactionData.Core.Interfaces
{
    public interface IDataExcelReader
    {
        IExcelDataReader GetExcelFile(string file);
        List<ExcelMessages> ProcessExcelFile(IExcelDataReader excelData, bool hasColumnNames);
        List<ExcelMessages> ValidateUpdatedData(List<string> transactionDataList);
    }
}