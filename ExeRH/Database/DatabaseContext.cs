using ExeRH.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TechnologySkill> Skills { get; internal set; }
        public DbSet<JobPosition> JobPositions { get; internal set; }
        public DbSet<JobPositionSkillAssignment> JobPositionSkillAssignments { get; internal set; }
        public DbSet<UserSkillAssignment> UserSkillAssignments { get; internal set; }
        public DbSet<ApplicantUser> Users { get; internal set; }
        public DbSet<Interview> Interviews { get; internal set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
