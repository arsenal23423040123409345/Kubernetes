using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _dbContext;

        public PlatformRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _dbContext.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
            => _dbContext.Platforms.ToList();

        public Platform? GetPlatformById(int id)
            => _dbContext.Platforms.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges()
            => _dbContext.SaveChanges() >= 0;
    }
}
