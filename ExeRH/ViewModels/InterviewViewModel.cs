using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.ViewModels
{
    public class InterviewViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        public JobPositionViewModel JobPosition { get; set; }
        public ApplicantUserViewModel User { get; set; }
        public List<TechnologySkillViewModel> Skills { get; set; }
    }
}
