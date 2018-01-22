using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Models
{
    public class ApplicantUser
    {
        [Key]
        public int Id { get; internal set; }

        [Required]
        [Display(Name = "Nome")]
        public string FullName { get; internal set; }

        [Display(Name = "Tecnologias")]
        public List<UserSkillAssignment> SkillAssignments { get; set; } = new List<UserSkillAssignment>();
    }
}
