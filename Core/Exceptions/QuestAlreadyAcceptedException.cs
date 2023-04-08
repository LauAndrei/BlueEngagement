namespace Core.Exceptions;

public class QuestAlreadyAcceptedException : Exception
{
    public QuestAlreadyAcceptedException(string message = "Quest already accepted!") : base(message)
    {
        
    }
}