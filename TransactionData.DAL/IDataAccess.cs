using System.Collections.Generic;
using System.Data;

namespace TransactionData.DAL
{
    public interface IDataAccess
    {
        List<Dictionary<string, object>> GetListTransactionData();
        DataTable InitDataTable();
        void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable excelFileData);
    }
}