using FinancialTrackerApi.Models;
using MongoDB.Driver;

namespace FinancialTrackerApi;

public class FinancialTrackerDbContext
{
    private readonly IMongoDatabase _database;
    public FinancialTrackerDbContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.AtlasUri);
        _database = client.GetDatabase(settings.DatabaseName);
    }
    public IMongoCollection<FinancialTransaction> Transactions => _database
        .GetCollection<FinancialTransaction>("Transactions");
}