namespace KardesAile.CommonTypes.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(int code, string message) : this(message)
    {
        Code = code;
    }

    public int Code { get; }
}