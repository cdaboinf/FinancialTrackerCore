using System.Transactions;
using FinancialTrackerApi.Models;
using MongoDB.Bson;

namespace FinancialTrackerApi.Services;

public interface ITransactionService
{
    Task<IEnumerable<FinancialTransaction>> GetTransactionsAsync();
    
    Task<FinancialTransaction> GetTransactionAsync(ObjectId id);
    
    Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction);
    
    Task<FinancialTransaction> UpdateTransactionAsync(FinancialTransaction transaction);
    
    Task<bool> DeleteTransactionAsync(ObjectId id);
}