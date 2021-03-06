﻿using System;
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

        [Display(Name = "Vaga")]
        public JobPositionViewModel JobPosition { get; set; }

        [Display(Name = "Candidato")]
        public ApplicantUserViewModel User { get; set; }

        [Display(Name = "Tecnologias")]
        public List<TechnologySkillViewModel> Skills { get; set; }
    }
}
