namespace Core.Exceptions;

public class NotEnoughPointsException : Exception
{
    public NotEnoughPointsException()
    {
        
    }

    public NotEnoughPointsException(string message = "You don't have enough points!") : base(message)
    {
        
    }
}