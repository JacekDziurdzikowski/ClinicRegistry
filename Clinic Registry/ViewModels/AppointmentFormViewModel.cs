using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Registry.ViewModels
{
    public class AppointmentFormViewModel
    {
        [Required]
        [Display(Name = "Full name of the doctor")]
        public string DoctorName { get; set; }
        [Required]
        [Display(Name = "Full name of the patient")]
        public string PatientName { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}