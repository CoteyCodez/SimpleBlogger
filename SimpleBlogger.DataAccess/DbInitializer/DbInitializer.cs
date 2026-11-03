using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Models;
using SimpleBlogger.Utilities;
using SimpleBlogger.Models;
using SimpleBlogger.Utilities;
using SimpleBlogger.Data;

namespace TaskManager.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            string[] tagNames =
            {
                SD.PersonalPortfolioTag,
                SD.BusinessCorporateTag,
                SD.LifestyleTag,
                SD.HealthFitnessTag,
                SD.FoodRecipeTag,
                SD.TravelTag,
                SD.FinanceMoneyTag,
                SD.ParentingFamilyTag,
                SD.TechnologyEntertainmentTag,
                SD.NicheAffiliateNewsTag
            };

            var existing = await _context.Tags.Select(t => t.Name).ToListAsync();
            var missing = tagNames.Except(existing).Select(n => new Tag { Name = n }).ToList();

            if (missing.Count > 0)
            {
                _context.Tags.AddRange(missing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
