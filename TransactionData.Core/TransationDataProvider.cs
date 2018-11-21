using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Core.Interfaces;
using TransactionData.DAL;
using TransactionData.Core.Model;

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

        public int Save(TransactionModel transaction)
        {
            try
            {
                DataTable excelDatatable = _dataAccess.InitDataTable();

                DataRow dataRow = excelDatatable.NewRow();
                // Use Trim to remove blank spaces
                dataRow["Account"] = transaction.Account.Trim();
                dataRow["Description"] = transaction.Description.Trim();
                dataRow["CurrencyCode"] = transaction.CurrencyCode.Trim();
                dataRow["Amount"] = transaction.Amount.ToString().Trim();
                excelDatatable.Rows.Add(dataRow);

                // Insert the current datatable to DB
                _dataAccess.InsertDataIntoSQLServerUsingSQLBulkCopy(excelDatatable);

                return 1;
            }
            catch (Exception)
            {
                throw;
            }    
        }
    }
}
