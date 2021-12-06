namespace PlatformService.Data 
{
    // class use for testing DbContext - Not production!
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app) 
        {
            // create a service-scoped db context for running the db context during testing

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // retrieve the scoped service of app db context
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
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