using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExeRH.Models;

namespace ExeRH.ViewModels
{
    public class JobPositionReportViewModel
    {
        public ApplicantUser User { get; internal set; }

        [Display(Name = "Pontuação")]
        public double TotalWeight { get; internal set; }
    }
}
