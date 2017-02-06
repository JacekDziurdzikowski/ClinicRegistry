using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clinic_Registry.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Registry.ViewModels
{
    public class AppointmentsListViewModel
    {
        public List<Appointment> AppointmentsList { get; set; }

        public List<Appointment> TempAppointmentsList { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Choose Date")]
        public DateTime Date { get; set; }
    }
}