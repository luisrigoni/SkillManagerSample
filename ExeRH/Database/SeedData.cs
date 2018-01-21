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
                SeedInterviews(context);
            }
        }

        private static void SeedInterviews(DatabaseContext context)
        {
            if (context.Interviews.Any())
            {
                return; // DB has already been seeded
            }

            context.Interviews.AddRange(
                new Interview
                {
                    Date = DateTime.Now.AddDays(-new Random().Next(1, 30)).AddSeconds(new Random().Next(1, 86400)),
                    JobPosition = context.JobPositions.First(),
                    User = context.Users.First(),
                },
                new Interview
                {
                    Date = DateTime.Now.AddDays(-new Random().Next(1, 30)).AddSeconds(new Random().Next(1, 86400)),
                    JobPosition = context.JobPositions.Last(),
                    User = context.Users.Last(),
                }
            );
            context.SaveChanges();
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
                },
                new JobPosition
                {
                    DisplayName = "Scrum Master",
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
                     FullName = "Norberto Maria",
                 },

                 new ApplicantUser
                 {
                     FullName = "Horácio Luiz",
                 },

                 new ApplicantUser
                 {
                     FullName = "Altair Fabricio",
                 },

                 new ApplicantUser
                 {
                     FullName = "Jordão Josué",
                 },

                 new ApplicantUser
                 {
                     FullName = "Bernardo Maximiano",
                 },

                 new ApplicantUser
                 {
                     FullName = "Salomão Luisinho",
                 },

                 new ApplicantUser
                 {
                     FullName = "Herberto Cezar",
                 }
            );
            context.SaveChanges();
        }
    }
}
