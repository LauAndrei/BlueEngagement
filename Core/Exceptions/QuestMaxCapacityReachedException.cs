namespace Core.Exceptions;

public class QuestMaxCapacityReachedException : Exception
{
    public QuestMaxCapacityReachedException(string message = "The maximum number of rewards for this quest has been reached!") : base(message)
    {
        
    }
}