namespace GHotel.Application.Exceptions;

public class NotFoundException : Exception
{
    public string Code { get; } = "NotFound";

    public NotFoundException(string message) : base(message)
    {
    }
}
