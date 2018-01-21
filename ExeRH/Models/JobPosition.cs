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
        public string DisplayName { get; set; }

        public List<TechnologySkill> RequiredSkills { get; internal set; }
    }
}
