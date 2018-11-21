using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TransactionData.DAL
{
    public class DataAccess : IDataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        /// <summary>
        /// Initialize a datatable containing transaction data
        /// Used with bulk insert into the DB
        /// </summary>
        /// <returns></returns>
        public DataTable InitDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable = new System.Data.DataTable("Transactions");
            dataTable.Columns.Add("Account", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("CurrencyCode", typeof(string));
            dataTable.Columns.Add("Amount", typeof(double));

            return dataTable;
        }

        /// <summary>
        /// Insert a datatable to the transaction table
        /// 
        /// </summary>
        /// <param name="excelFileData"></param>
        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable excelFileData)
        {
            // Connect to DB and open connection
            SqlConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            // Nulk copy the correct data to DB
            using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
            {
                // Use table named Transactions
                s.DestinationTableName = "Transactions";
                foreach (var column in excelFileData.Columns)
                    s.ColumnMappings.Add(column.ToString(), column.ToString());
                // Write to DB
                s.WriteToServer(excelFileData);
            }
            dbConnection.Close();
        }

        /// <summary>
        /// Return list of dictionnary containing all transaction data
        /// </summary>
        /// <returns>list of dictionarry containing all transaction data from DB</returns>
        public DataTable GetListTransactionData()
        {
            DataTable dt = new DataTable();
            // Connect to DB and open connection
            SqlConnection dbConnection = new SqlConnection(connectionString);

            // SQL query to return all data from Transactions table
            using (SqlCommand cmd = new SqlCommand("SELECT Id,Account,Description,CurrencyCode,Amount FROM Transactions"))
            {
                dbConnection.Open();
                cmd.Connection = dbConnection;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
        }

        public bool UpdateTransaction(string transactionId, List<string> transaction)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE Transactions SET Account=@Account,Description=@Description,CurrencyCode=@CurrencyCode,Amount=@Amount WHERE Id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Account", (transaction[0].ToString().Trim()));
                    sqlCmd.Parameters.AddWithValue("@Description", (transaction[1].ToString().Trim()));
                    sqlCmd.Parameters.AddWithValue("@CurrencyCode", (transaction[2].ToString().Trim()));
                    sqlCmd.Parameters.AddWithValue("@Amount", (transaction[3].ToString().Trim()));
                    sqlCmd.Parameters.AddWithValue("@id", transactionId);
                    sqlCmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }  
        }

        public bool DeleteTransaction(string transactionId)
        {
            try
            {
                 using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM Transactions WHERE Id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", transactionId);
                    sqlCmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
