using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestController : ControllerBase
{
    [HttpGet]
    [Route("GetAllQuests")]
    public string GetAllQuests()
    {
        return "This will be a list of quests";
    }
}