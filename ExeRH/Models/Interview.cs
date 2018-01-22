using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Models
{
    public class Interview
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public ApplicantUser User { get; set; }

        [Required]
        [Display(Name = "Candidato")]
        public int UserId { get; set; }

        public JobPosition JobPosition { get; set; }

        [Required]
        [Display(Name = "Vaga")]
        public int JobPositionId { get; set; }
    }
}
