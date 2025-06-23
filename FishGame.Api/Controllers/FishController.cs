using Microsoft.AspNetCore.Mvc;
using FishGame.Models;
using FishGame.Services;

namespace FishGame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FishController : ControllerBase
{
    private readonly FishDataService _fishDataService;

    public FishController(FishDataService fishDataService)
    {
        _fishDataService = fishDataService;
    }

    // GET: api/fish/{userId}
    [HttpGet("{userId}")]
    public ActionResult<Fish> GetFish(string userId)
    {
        var fish = _fishDataService.LoadFish(userId);
        if (fish == null)
        {
            return NotFound($"No fish found for user {userId}");
        }
        return Ok(fish);
    }

} 