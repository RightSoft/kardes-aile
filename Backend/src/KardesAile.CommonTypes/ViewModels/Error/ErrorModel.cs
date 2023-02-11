namespace KardesAile.CommonTypes.ViewModels.Error;

public class ErrorModel
{
    public int Code { get; set; }
    public string Error { get; set; } = null!;
    public int StatusCode { get; set; }
}