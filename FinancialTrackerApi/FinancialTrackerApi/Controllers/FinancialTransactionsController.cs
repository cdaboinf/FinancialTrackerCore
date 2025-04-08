using System.Globalization;
using System.Transactions;
using FinancialTrackerApi.Models;
using FinancialTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FinancialTrackerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancialTransactionsController(
    ILogger<FinancialTransactionsController> logger, ITransactionService transactionService) : ControllerBase
{
    private readonly ILogger<FinancialTransactionsController> _logger = logger;

    [HttpGet(Name = "GetFinancialTransaction")]
    [Authorize]
    public async Task<IEnumerable<FinancialTransaction>> Get()
    {
        /*var transactions = Enumerable.Range(1, 5).Select(index => new FinancialTransaction
            {
                Id = new ObjectId(),
                Date = DateTime.Now.AddDays(index),
                Amount = index,
                CurrencyAmount = index.ToString("C", new CultureInfo("en-US")),
                Vendor = "Amazon",
                Type = TransactionType.Personal
            })
            .ToArray();*/
        var transactions = await transactionService.GetTransactionsAsync();
        return transactions;
    }
}