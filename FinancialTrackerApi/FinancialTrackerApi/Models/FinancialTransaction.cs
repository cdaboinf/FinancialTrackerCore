using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;

namespace FinancialTrackerApi.Models;

public class FinancialTransaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonElement("userId")]
    public Guid UserId { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }
    
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
    
    [BsonElement("description")]
    public string? Description { get; set; }
    
    [BsonElement("category")]
    public string? Category { get; set; }
    
    [BsonElement("amount")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Amount { get; set; }
    
    public string? CurrencyAmount { get; set; }
    
    [BsonElement("vendor")]
    public string? Vendor { get; set; }
    
    [BsonElement("type")]
    public TransactionType Type { get; set; }
}