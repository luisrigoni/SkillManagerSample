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

            var dotnet = new JobPosition
            {
                DisplayName = "Desenvolvedor .NET",
            };
            var angular = new JobPosition
            {
                DisplayName = "Desenvolvedor Front-End",
            };
            var scrum = new JobPosition
            {
                DisplayName = "Scrum Master",
            };

            context.JobPositions.AddRange(dotnet, angular, scrum);
            context.SaveChanges();

            context.JobPositionsSkills.AddRange(
                new JobPositionSkillAssignment
                {
                    JobPosition = dotnet,
                    Skill = context.Skills.First(),
                },
                new JobPositionSkillAssignment
                {
                    JobPosition = dotnet,
                    Skill = context.Skills.Last(),
                },
                new JobPositionSkillAssignment
                {
                    JobPosition = angular,
                    Skill = context.Skills.Last(),
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
                    DislayName = "C# (C-Sharp)",
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
