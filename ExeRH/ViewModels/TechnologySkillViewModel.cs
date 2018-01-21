using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExeRH.Models;

namespace ExeRH.ViewModels
{
    public class TechnologySkillViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [Display(Name = "Nome")]
        public string DisplayName { get; set; }
    }
}
