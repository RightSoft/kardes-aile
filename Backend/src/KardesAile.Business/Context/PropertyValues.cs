namespace KardesAile.Business.Context;

public class PropertyValues
{
    public PropertyValues(string name, object? original, object? current)
    {
        Name = name;
        Current = current?.ToString();
        Original = original?.ToString();
    }

    public string Name { get; }
    public string? Current { get; }
    public string? Original { get; }
}