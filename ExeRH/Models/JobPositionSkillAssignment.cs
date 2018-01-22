using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Models
{
    public class JobPositionSkillAssignment
    {
        [Key]
        public int Id { get; set; }

        public JobPosition JobPosition { get; set; }

        [Required]
        public int JobPositionId { get; set; }

        public TechnologySkill Skill { get; set; }

        [Required]
        public int SkillId { get; set; }

    }
}
