using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clinic_Registry.Models;

namespace Clinic_Registry.ViewModels
{
    public class PatientsListViewModel
    {
        public List<Patient> PatientsList { get; set; }
    }
}