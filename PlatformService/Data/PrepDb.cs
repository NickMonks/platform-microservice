using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data 
{
    // class use for testing DbContext - Not production!
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd) 
        {
            // create a service-scoped db context for running the db context during testing

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // retrieve the scoped service of app db context
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            // if we work in production, please apply migrations
            if (isProd) 
            {
                Console.WriteLine("---> Attempting to apply migrations ...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"---> Could not run migrations: {e.Message}");
                }
                
            } 

            if(!context.Platforms.Any())
            {
                Console.WriteLine("---> Seeding data ...");

                // use range to add collection of Platform
                context.Platforms.AddRange(
                    new Models.Platform() {Name="Dot Net", Publisher="Microsoft", Cost="Free"},
                    new Models.Platform() {Name="SQL", Publisher="Microsoft", Cost="Free"},
                    new Models.Platform() {Name="K8S", Publisher="Cloud Native", Cost="Free"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> We already have data");
            }
        }
    }
}