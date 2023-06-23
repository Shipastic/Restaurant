using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Restaurant.Services.Identity.DbContexts;
using Restaurant.Services.Identity.Models;
using System.Security.Claims;

namespace Restaurant.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else { return; }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "HeadAdmin",
                Email = "admin@admin.com",
                EmailConfirmed = false,
                PhoneNumber = "88005553535",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 1,
                FirstName = "Den",
                LastName = "Shipelov"
            };

            _userManager.CreateAsync(adminUser, "Admin1234*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin),
            }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customerUser",
                Email = "customer@customer.com",
                EmailConfirmed = false,
                PhoneNumber = "88005553566",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 1,
                FirstName = "Ned",
                LastName = "Volepihs"
            };

            _userManager.CreateAsync(customerUser, "Customer1234*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer),
            }).Result;
        }
    }
}
