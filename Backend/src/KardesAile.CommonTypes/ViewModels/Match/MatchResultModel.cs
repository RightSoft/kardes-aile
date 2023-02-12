namespace KardesAile.CommonTypes.ViewModels.Match;

public class MatchResultModel
{
    public Guid VictimId { get; set; }
    public string VictimFirstName { get; set; } = null!;
    public string VictimLastName { get; set; } = null!;

    public Guid SupporterId { get; set; }
    public string SupporterFirstName { get; set; } = null!;
    public string SupporterLastName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
    public Guid? VictimChildId { get; set; }
    public string? VictimChildName { get; set; }
    public Guid? SupporterChildId { get; set; }
    public string? SupporterChildName { get; set; }
}