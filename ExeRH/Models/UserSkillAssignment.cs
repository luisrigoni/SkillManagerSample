using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Models
{
    public class UserSkillAssignment
    {
        [Key]
        public int Id { get; set; }

        public ApplicantUser User { get; set; }

        [Required]
        public int UserId { get; set; }

        public TechnologySkill Skill { get; set; }

        [Required]
        public int SkillId { get; set; }

    }
}
