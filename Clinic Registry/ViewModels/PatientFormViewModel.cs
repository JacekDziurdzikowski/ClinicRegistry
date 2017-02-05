using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clinic_Registry.Models;

namespace Clinic_Registry.ViewModels
{
    public class PatientFormViewModel
    {
        public List<Gender> Genders { get; set; }
        public Patient Patient { get; set; }
    }
}