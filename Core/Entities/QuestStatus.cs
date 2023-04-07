using System.Runtime.Serialization;

namespace Core.Entities;

public enum QuestStatus
{
    [EnumMember(Value = "Accepted")]
    Accepted,
    
    [EnumMember(Value = "Completed")]
    Completed
}