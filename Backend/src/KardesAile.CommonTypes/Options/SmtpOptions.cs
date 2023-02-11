using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.Options;

public class SmtpOptions
{
    [Required] public string? FromAddress { get; set; }

    [Required] public string? FromName { get; set; }

    [Required] public string? Server { get; set; }

    [Required] public int? Port { get; set; }

    [Required] public string? Username { get; set; }

    [Required] public string? Password { get; set; }
}