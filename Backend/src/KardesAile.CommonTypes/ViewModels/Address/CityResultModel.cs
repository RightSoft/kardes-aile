namespace KardesAile.CommonTypes.ViewModels.Address;

public class CityResultModel
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string StateCode { get; set; } = null!;
}