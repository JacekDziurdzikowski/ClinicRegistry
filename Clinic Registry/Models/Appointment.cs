using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic_Registry.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime Date { get; set; }
    }
}