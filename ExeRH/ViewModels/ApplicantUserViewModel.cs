using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.ViewModels
{
    public class ApplicantUserViewModel
    {
        public int Id { get; internal set; }
        [Required]
        [MinLength(10)]
        [Display(Name = "Nome completo")]
        public string FullName { get; internal set; }
    }
}
