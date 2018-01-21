using ExeRH.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Database
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>()))
            {
                SeedSkills(context);
                SeedUsers(context);
                SeedJobPositions(context);
            }
        }

        private static void SeedJobPositions(DatabaseContext context)
        {
            if (context.JobPositions.Any())
            {
                return; // DB has already been seeded
            }

            context.JobPositions.AddRange(
                new JobPosition
                {
                    DisplayName = "Desenvolvedor .NET",
                },
                new JobPosition
                {
                    DisplayName = "Desenvolvedor Angular 2+",
                }
            );
            context.SaveChanges();
        }

        private static void SeedSkills(DatabaseContext context)
        {
            if (context.Skills.Any())
            {
                return; // DB has already been seeded
            }

            context.Skills.AddRange(
                new TechnologySkill
                {
                    DislayName = "C# (CSharp)",
                },
                new TechnologySkill
                {
                    DislayName = "ASP.NET MVC",
                },
                new TechnologySkill
                {
                    DislayName = "Entity Framework",
                },
                new TechnologySkill
                {
                    DislayName = "Angular 2+",
                }
            );
            context.SaveChanges();
        }

        private static void SeedUsers(DatabaseContext context)
        {
            if (context.Users.Any())
            {
                return; // DB has already been seeded
            }

            context.Users.AddRange(
                 new ApplicantUser
                 {
                     Name = "Norberto Maria",
                     AvatarURL = "https://robohash.org/VVG.png?set=set1&size=150x150",
                 },

                 new ApplicantUser
                 {
                     Name = "Horácio Luiz",
                     AvatarURL = "https://robohash.org/J47.png?set=set1&size=150x150",
                 },

                 new ApplicantUser
                 {
                     Name = "Altair Fabricio",
                     AvatarURL = "https://robohash.org/AF0.png?set=set1&size=150x150",
                 },

                 new ApplicantUser
                 {
                     Name = "Jordão Josué",
                     AvatarURL = "https://robohash.org/9GI.png?set=set2&size=150x150",
                 },

                 new ApplicantUser
                 {
                     Name = "Bernardo Maximiano",
                     AvatarURL = "https://robohash.org/2XJ.png?set=set2&size=150x150",
                 },

                 new ApplicantUser
                 {
                     Name = "Salomão Luisinho",
                     AvatarURL = "https://robohash.org/ZHL.png?set=set3&size=150x150",
                 },

                 new ApplicantUser
                 {
                     Name = "Herberto Cezar",
                     AvatarURL = "https://robohash.org/X6M.png?set=set3&size=150x150",
                 }
            );
            context.SaveChanges();
        }
    }
}
