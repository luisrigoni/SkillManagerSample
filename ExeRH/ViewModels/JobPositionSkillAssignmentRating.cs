using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.ViewModels
{
    public class JobPositionSkillAssignmentRatingViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Peso da tecnologia")]
        public double? Weight { get; internal set; }
        public int JobPositionId { get; internal set; }
        public int SkillId { get; internal set; }
        [Display(Name = "Tecnologia")]
        public string SkillName { get; internal set; }
    }
}
