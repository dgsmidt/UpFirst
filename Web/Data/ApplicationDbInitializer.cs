using Microsoft.AspNetCore.Identity;

namespace Web.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            string defaultPwd = "Admin@1234";

            if (userManager.FindByEmailAsync("admin@admin").Result == null)
            {
                ApplicationUser user1 = new ApplicationUser
                {
                    UserName = "admin@admin",
                    Email = "admin@admin",
                    EmailConfirmed = true,
                    Nome = "Admin"
                };

                IdentityResult result1 = userManager.CreateAsync(user1, defaultPwd).Result;

                if (result1.Succeeded)
                {
                    userManager.AddToRoleAsync(user1, "Administrator").Wait();
                    userManager.AddClaimAsync(user1, new System.Security.Claims.Claim("Nome", user1.Nome));
                }
            }

            //if (userManager.FindByEmailAsync("daniel.smidt@yahoo.com.br").Result == null)
            //{
            //    ApplicationUser user2 = new ApplicationUser
            //    {
            //        UserName = "daniel.smidt@yahoo.com.br",
            //        Email = "daniel.smidt@yahoo.com.br",
            //        EmailConfirmed = true,
            //        Nome = "Daniel"
            //    };

            //    IdentityResult result1 = userManager.CreateAsync(user2, defaultPwd).Result;

            //    if (result1.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user2, "Administrator").Wait();
            //        userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("Nome", user2.Nome));
            //    }
            //}

            //if (userManager.FindByEmailAsync("csrclaudio@gmail.com").Result == null)
            //{
            //    ApplicationUser user3 = new ApplicationUser
            //    {
            //        UserName = "csrclaudio@gmail.com",
            //        Email = "csrclaudio@gmail.com",
            //        EmailConfirmed = true,
            //        Nome = "Cláudio"
            //    };

            //    IdentityResult result1 = userManager.CreateAsync(user3, "Csr@1234").Result;

            //    if (result1.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user3, "Administrator").Wait();
            //        userManager.AddClaimAsync(user3, new System.Security.Claims.Claim("Nome", user3.Nome));
            //    }
            //}
        }
    }
}
