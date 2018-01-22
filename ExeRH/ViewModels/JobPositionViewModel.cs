using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.ViewModels
{
    public class JobPositionViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [Display(Name = "Nome da vaga")]
        public string DisplayName { get; set; }

        [Display(Name = "Tecnologias")]
        public List<TechnologySkillViewModel> Skills { get; set; }
    }
}
