using System.Collections.Generic;
using TransactionData.Core.Messeges;

namespace TransactionData.Core
{
    public interface ITransactionProcess
    {
        List<ExcelMessages> ValidateExcelFirstRow(string[] values);
        void Process(string[] values);
        int ValidateExcelContent(string[] values);
    }
}