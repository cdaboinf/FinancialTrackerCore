namespace AlkamiTests;

public class QueryConverter
{
    // Converts a basic SQL-like query into a LINQ query (Lazy Evaluation)
    public IQueryable<Product> ConvertToLinq(string query, IQueryable<Product> data)
    {
        if (query.Contains("WHERE"))
        {
            var condition = query.Split("WHERE")[1].Trim();

            if (condition.Contains("Price >"))
            {
                var priceValue = decimal.Parse(condition.Split('>')[1].Trim());
                return data.Where(p => p.Price > priceValue);
            }
            else if (condition.Contains("Name ="))
            {
                var nameValue = condition.Split('=')[1].Trim().Replace("'", "");
                return data.Where(p => p.Name.Equals(nameValue, StringComparison.OrdinalIgnoreCase));
            }
        }

        return data; // Return full list if no condition found
    }
}