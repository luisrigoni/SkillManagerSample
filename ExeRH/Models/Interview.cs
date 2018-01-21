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
        [Required]
        public ApplicantUser User { get; set; }
        [Required]
        public JobPosition JobPosition { get; set; }
    }
}
