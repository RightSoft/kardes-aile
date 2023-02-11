namespace KardesAile.Database.Abstracts;

public interface IEntity
{
    Guid Id { get; }
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
    uint Version { get; }
}