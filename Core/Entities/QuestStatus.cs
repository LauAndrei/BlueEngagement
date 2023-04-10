using System.Runtime.Serialization;

namespace Core.Entities;

public enum QuestStatus
{
    /// <summary>
    ///     This will be used when retrieving the quest
    ///   status for a user.
    /// </summary>
    [EnumMember(Value = "NotAccepted")]
    NotAccepted,
    
    [EnumMember(Value = "Accepted")]
    Accepted,
    
    [EnumMember(Value = "Completed")]
    Completed
}