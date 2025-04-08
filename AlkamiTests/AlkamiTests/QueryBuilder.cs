namespace AlkamiTests;

public class QueryBuilder
{
    private string _select;
    private string _from;
    private string _where;
    private string _orderBy;
    private string _name;

    public QueryBuilder(string name)
    {
        _name = name;
    }

    public QueryBuilder Select(params string[] columns)
    {
        _select = "SELECT " + string.Join(", ", columns);
        return this;
    }

    public QueryBuilder From(string table)
    {
        _from = "FROM " + table;
        return this;
    }

    public QueryBuilder Where(string condition)
    {
        _where = "WHERE " + condition;
        return this;
    }

    public QueryBuilder OrderBy(string column)
    {
        _orderBy = "ORDER BY " + column;
        return this;
    }

    public string Execute()
    {
        return $"{_select} {_from} {_where} {_orderBy}";
    }
}