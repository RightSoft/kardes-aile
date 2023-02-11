using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Cocuk : BaseEntity
{
    public string Ad { get; set; } = null!;
    public DateTime DogumTarih { get; set; }
    public Cinsiyet Cinsiyet { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}