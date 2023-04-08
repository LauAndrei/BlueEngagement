namespace Core.Exceptions;

public class QuestAlreadyCompletedException : Exception
{
    public QuestAlreadyCompletedException(string message = "You have already completed this quest!") : base(message)
    {
        
    }
}