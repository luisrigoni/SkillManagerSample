using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExeRH.Models;

namespace ExeRH.ViewModels
{
    public class JobPositionIndexViewModel
    {
        public int Id { get; internal set; }

        [Display(Name = "Vaga")]

        public string DisplayName { get; internal set; }

        [Display(Name = "Tecnologias")]
        public List<TechnologySkill> Skills { get; internal set; }

        [Display(Name = "Candidaturas")]
        public int Appliances { get; internal set; }
    }
}
