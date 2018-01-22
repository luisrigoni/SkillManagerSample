using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Models
{
    public class JobPosition
    {
        [Key]
        public int Id { get; internal set; }

        [Required]
        [Display(Name = "Vaga")]
        public string DisplayName { get; internal set; }

        [Display(Name = "Tecnologias")]
        public List<JobPositionSkillAssignment> JobPositionSkillsAssignments { get; set; } = new List<JobPositionSkillAssignment>();
    }
}
