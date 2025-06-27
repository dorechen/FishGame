using Microsoft.AspNetCore.Mvc;
using FishGame.Models;
using FishGame.Core.Repositories;

namespace FishGame.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FishController : ControllerBase
    {
        private readonly IFishRepository _fishRepository;
        private const string DEFAULT_USER = "default";

        public FishController(IFishRepository fishRepository)
        {
            _fishRepository = fishRepository;
        }

        // GET: api/fish/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<Fish>> GetFish(string userId)
        {
            var fish = await _fishRepository.GetFishByUserIdAsync(userId);
            if (fish == null)
            {
                return NotFound($"No fish found for user {userId}");
            }
            return Ok(fish);
        }

        // POST: api/fish/{userId}
        [HttpPost("{userId}")]
        public async Task<ActionResult<Fish>> CreateFish(string userId)
        {
            try
            {
                // Create a new fish with default starting position
                var newFish = new Fish(
                    startX: 10,
                    startY: 5,
                    facingRight: true,
                    color: ConsoleColor.Blue
                );

                var createdFish = await _fishRepository.CreateFishAsync(userId, newFish);
                return CreatedAtAction(nameof(GetFish), new { userId }, createdFish);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PUT: api/fish/{userId}
        [HttpPut("{userId}")]
        public async Task<ActionResult<Fish>> UpdateFish(string userId, Fish fish)
        {
            try
            {
                var updatedFish = await _fishRepository.UpdateFishAsync(fish);
                return Ok(updatedFish);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/fish/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteFish(string userId)
        {
            var fish = await _fishRepository.GetFishByUserIdAsync(userId);
            if (fish == null)
            {
                return NotFound($"No fish found for user {userId}");
            }

            await _fishRepository.DeleteFishAsync(fish.Id);
            return NoContent();
        }
    }
} 