using TransactionData.Core.Model;

namespace TransactionData.Core.Interfaces
{
    public interface ITransactionDataProvider
    {
        int Save(TransactionModel transaction);
    }
}
