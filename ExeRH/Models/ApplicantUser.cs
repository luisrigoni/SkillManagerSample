using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExeRH.Models
{
    public class ApplicantUser
    {
        [Key]
        public int Id { get; internal set; }
        [Required]
        public string Name { get; internal set; }
        public string AvatarURL { get; internal set; }
    }
}
