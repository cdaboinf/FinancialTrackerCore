using System.Runtime.InteropServices;

namespace FinancialTrackerApi.Models;

public enum TransactionType
{
    Unknown,
    Subscription,
    Mortgage,
    Insurance,
    Groceries,
    Gas,
    CarPayment,
    Family,
    Personal
}