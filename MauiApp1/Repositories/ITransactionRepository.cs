using MauiApp1.Models;

namespace MauiApp1.Repositories
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        void Delete(Transaction transaction);
        List<Transaction> GetAll();
        void Update(Transaction transaction);
    }
}