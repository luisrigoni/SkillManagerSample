using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.ViewModels
{
    public class InterviewDetailsViewModel
    {
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Vaga")]
        public string JobPositionName { get; set; }

        [Display(Name = "Candidato")]
        public string UserName { get; set; }

        [Display(Name = "Tecnologias")]
        public List<AssignedSkillData> Skills { get; set; } = new List<AssignedSkillData>();
    }
}
