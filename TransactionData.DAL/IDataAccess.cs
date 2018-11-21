using System.Collections.Generic;
using System.Data;

namespace TransactionData.DAL
{
    public interface IDataAccess
    {
        DataTable GetListTransactionData();
        DataTable InitDataTable();
        void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable excelFileData);
        bool UpdateTransaction(string transactionId, List<string> transaction);
        bool DeleteTransaction(string transactionId);
    }
}