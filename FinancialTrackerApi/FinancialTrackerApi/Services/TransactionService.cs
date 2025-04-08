using MongoDB.Driver;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FinancialTrackerApi.Models;
using MongoDB.Bson;

namespace FinancialTrackerApi.Services;

public class TransactionService(FinancialTrackerDbContext dbContext) : ITransactionService
{
    private readonly IMongoCollection<FinancialTransaction> _transactions = dbContext.Transactions;

    public async Task<IEnumerable<FinancialTransaction>> GetTransactionsAsync()
    {
        var transactions = await _transactions.Find(_ => true).ToListAsync();
        foreach (var financialTransaction in transactions)
        {
            financialTransaction.CurrencyAmount = financialTransaction.Amount
                .ToString("C", new CultureInfo("en-US"));
        }
        return transactions;
    }

    public Task<FinancialTransaction> GetTransactionAsync(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public async Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction)
    {
       await _transactions.InsertOneAsync(transaction);
       return transaction;
    }

    public Task<FinancialTransaction> UpdateTransactionAsync(FinancialTransaction transaction)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteTransactionAsync(ObjectId id)
    {
        var result = await _transactions.DeleteOneAsync(t => t.Id == id);
        return result.DeletedCount > 0;
    }
}