using TransactionData.Core.Model;

namespace TransactionData.Core.Interfaces
{
    public interface ITransactionDataProvider
    {
        void Save(string[] values);

        int Save(TransactionModel transaction);


    }
}
