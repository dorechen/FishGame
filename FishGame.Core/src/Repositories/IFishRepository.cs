using FishGame.Models;

namespace FishGame.Core.Repositories
{
    public interface IFishRepository
    {
        // Get fish by user id (currently hardcoded as "default")
        Task<Fish?> GetFishByUserIdAsync(string userId);
        
        // Create a new fish for user
        Task<Fish> CreateFishAsync(string userId, Fish fish);
        
        // Update existing fish
        Task<Fish> UpdateFishAsync(Fish fish);
        
        // Delete fish
        Task DeleteFishAsync(Guid fishId);
    }
} 