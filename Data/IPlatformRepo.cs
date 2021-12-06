using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        // Repository pattern - Use a common interface to encapsulate data access layer
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
        
    }
}