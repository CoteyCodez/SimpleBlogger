using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Data;
using SimpleBlogger.Models;
using SimpleBlogger.Models;
using SimpleBlogger.Utilities;
using SimpleBlogger.Utilities;

namespace TaskManager.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitializer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            if (!await _roleManager.RoleExistsAsync(SD.RoleAuthor))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.RoleAuthor));
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
