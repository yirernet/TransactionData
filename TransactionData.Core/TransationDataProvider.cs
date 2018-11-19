using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Core.Interfaces;
using TransactionData.DAL;

namespace TransactionData.Core
{
    public class TransationDataProvider : ITransactionDataProvider
    {
        private readonly IDataAccess _dataAccess;

        public TransationDataProvider()
            : this(new DataAccess())
        {
        }

        public TransationDataProvider(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void Save(string[] values)
        {
            DataTable excelDatatable = _dataAccess.InitDataTable();

            DataRow dataRow = excelDatatable.NewRow();
            // Use Trim to remove blank spaces
            dataRow["Account"] = values[0].Trim();
            dataRow["Description"] = values[1].Trim();
            dataRow["CurrencyCode"] = values[2].Trim();
            dataRow["Amount"] = values[3].Trim();
            excelDatatable.Rows.Add(dataRow);

            // Insert the current datatable to DB
            _dataAccess.InsertDataIntoSQLServerUsingSQLBulkCopy(excelDatatable);
        }

    }
}
