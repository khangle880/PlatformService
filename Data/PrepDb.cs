using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void prepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                seedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void seedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("---> Seeding data...");

                context.Platforms.AddRange(
                    new Platform() { Name = "dot net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native computing Foundation", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }

        internal static void prepPopulation()
        {
            throw new NotImplementedException();
        }
    }
}