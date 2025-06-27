using Microsoft.EntityFrameworkCore;
using FishGame.Models;
using FishGame.Core.Data;

namespace FishGame.Core.Repositories
{
    public class FishRepository : IFishRepository
    {
        private readonly FishDbContext _context;

        public FishRepository(FishDbContext context)
        {
            _context = context;
        }

        public async Task<Fish?> GetFishByUserIdAsync(string userId)
        {
            // assuming one fish per user
            // Later we could add a UserId property to Fish if needed
            return await _context.Fish.FirstOrDefaultAsync();
        }

        public async Task<Fish> CreateFishAsync(string userId, Fish fish)
        {
            var existingFish = await GetFishByUserIdAsync(userId);
            if (existingFish != null)
            {
                throw new InvalidOperationException($"Fish already exists for user {userId}");
            }

            await _context.Fish.AddAsync(fish);
            await _context.SaveChangesAsync();
            return fish;
        }

        public async Task<Fish> UpdateFishAsync(Fish fish)
        {
            var existingFish = await _context.Fish.FindAsync(fish.Id);
            if (existingFish == null)
            {
                throw new InvalidOperationException($"Fish with ID {fish.Id} not found");
            }

            // Update all properties
            existingFish.X = fish.X;
            existingFish.Y = fish.Y;
            existingFish.Color = fish.Color;
            existingFish.isFacingRight = fish.isFacingRight;
            existingFish.fullness = fish.fullness;
            existingFish.LastUpdate = DateTime.UtcNow;

            _context.Fish.Update(existingFish);
            await _context.SaveChangesAsync();
            return existingFish;
        }

        public async Task DeleteFishAsync(Guid fishId)
        {
            var fish = await _context.Fish.FindAsync(fishId);
            if (fish != null)
            {
                _context.Fish.Remove(fish);
                await _context.SaveChangesAsync();
            }
        }
    }
} 