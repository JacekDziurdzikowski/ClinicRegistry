using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Clinic_Registry.ViewModels;
using Clinic_Registry.Models;


namespace Clinic_Registry.Controllers
{
    public class AppointmentFormCustomValidation
    {
        public static bool Validate( ref AppointmentFormViewModel viewModel, ref Appointment appointment)
        {
            using (var _context = new ApplicationDbContext())
            {
                var doctorsList = _context.Doctors.ToList();
                var patientsList = _context.Patients.ToList();

                bool existDoctor = false;
                bool existPatient = false;

                int doctorId = 0;
                int patientId = 0;

                //checking if the loaded doctor's name exists in DB
                foreach (var doctor in doctorsList)
                {
                    if (doctor.Name == viewModel.DoctorName)
                    {
                        existDoctor = true;
                        doctorId = doctor.Id;
                        break;
                    }

                }

                //checking if the loaded patient's name exists in DB
                foreach (var patient in patientsList)
                {
                    if (patient.Name == viewModel.PatientName)
                    {
                        existPatient = true;
                        patientId = patient.Id;
                        break;
                    }

                }

                if (existDoctor == true && existPatient == true)
                {

                    appointment.DoctorId = doctorId;
                    appointment.PatientId = patientId;
                    appointment.Date = viewModel.Date;

                    return true;
                }
                else
                {
                    if (existDoctor != true)
                        viewModel.DoctorName = "Enter the name of the doctor again";
                    if (existPatient != true)
                        viewModel.PatientName = "Enter the name of the patient again";
                    return false;
                }
            }
        }
    }
}