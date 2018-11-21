using System.Collections.Generic;
using System.Data;
using TransactionData.Core.Model;

namespace TransactionData.Core.Interfaces
{
    public interface ITransactionDataProvider
    {
        int Save(TransactionModel transaction);
        DataTable Get();
        bool Update(string transactionId, List<string> transaction);
        bool Delete(string transactionId);      
    }
}
