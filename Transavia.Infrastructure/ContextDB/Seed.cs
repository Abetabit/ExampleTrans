using Microsoft.AspNetCore.Identity;
using System;
using Transavia.Core.Entities;
using Transavia.Core.Enums;

namespace Transavia.Infrastructure.ContextDB
{
    public static class Seed
    {
        public async static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "Administrator".ToUpper()
                };

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                };

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public async static void SeedUsers(UserManager<User> userManager)
        {

            if (userManager.FindByEmailAsync("admin@betabit.nl").Result == null)
            {
                User user = new User
                {
                    Email = "admin@betabit.nl",
                    EmailConfirmed = true,
                    FirstName = "Bas",
                    LastName = "van der Meer",
                    PhoneNumber = "0612345678",
                    UserName = "admin@betabit.nl",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1991 - 05 - 03),
                };
                string password = "12345678";
                IdentityResult userResult = userManager.CreateAsync(user, password).Result;
                IdentityResult roleResult = userManager.AddToRoleAsync(user, "Administrator").Result;
            }
            if (userManager.FindByEmailAsync("user@betabit.nl").Result == null)
            {
                User user = new User
                {
                    Email = "user@betabit.nl",
                    EmailConfirmed = true,
                    FirstName = "Paula",
                    LastName = "Straat",
                    PhoneNumber = "0612345678",
                    UserName = "user@betabit.nl",
                    Gender = Gender.Female,
                    DateOfBirth = new DateTime(1991 - 05 - 03)
                };

                string password = "12345678";
                IdentityResult userResult = userManager.CreateAsync(user, password).Result;
                IdentityResult roleResult = userManager.AddToRoleAsync(user, "User").Result;
            }

            if (userManager.FindByEmailAsync("user2@betabit.nl").Result == null)
            {
                User user = new User
                {
                    Email = "user2@betabit.nl",
                    EmailConfirmed = true,
                    FirstName = "Luke",
                    LastName = "de Leeuw",
                    PhoneNumber = "0612345678",
                    UserName = "user2@betabit.nl",
                    Gender = Gender.Female,
                    DateOfBirth = new DateTime(1991 - 05 - 03)
                };

                string password = "12345678";
                IdentityResult userResult = userManager.CreateAsync(user, password).Result;
                IdentityResult roleResult = userManager.AddToRoleAsync(user, "User").Result;
            }
        }
    }
}
