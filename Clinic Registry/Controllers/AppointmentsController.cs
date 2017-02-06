using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinic_Registry.Models;
using Clinic_Registry.ViewModels;
using System.Data.Entity;

namespace Clinic_Registry.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext _context;
        private AppointmentsListViewModel appointments;


        public AppointmentsController()
        {
            _context = new ApplicationDbContext();
            appointments = new AppointmentsListViewModel
            {
                AppointmentsList = _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList(),
                TempAppointmentsList = new List<Appointment>()
            };
        } 

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }




        //Action supports displaying the appointments list
        public ActionResult Index(AppointmentsListViewModel appointmentsDateTransfer)
        {  
            //Checking for a date
            if(appointmentsDateTransfer.DateToDisplay == DateTime.MinValue)
            {
                appointments.DateToDisplay = DateTime.Today;
            }
            else
            {
                appointments.DateToDisplay = appointmentsDateTransfer.DateToDisplay;
            }

            //Choosing appointments with the valid date
            appointments.TempAppointmentsList.Clear();
            foreach (var appointment in appointments.AppointmentsList)
            {
                if (appointment.Date.Date == appointments.DateToDisplay.Date)
                    appointments.TempAppointmentsList.Add(appointment);
            }

            foreach (var appointment in appointments.TempAppointmentsList)
            {
                appointment.Doctor = _context.Doctors.SingleOrDefault(d => d.Id == appointment.DoctorId);
                appointment.Patient = _context.Patients.SingleOrDefault(p => p.Id == appointment.PatientId);
            }

            return View("AppointmentsList", appointments);
        }


        //Action supports displaying the form for create new appointment
        public ActionResult CreateNew(AppointmentFormViewModel appointment)
        {
            if (appointment == null)
                appointment = new AppointmentFormViewModel();

            return View("AppointmentForm", appointment);
        }


        //Action saves Appointment to Database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AppointmentFormViewModel viewModel)
        {

            var appointment = new Appointment();

            if (AppointmentFormCustomValidation.Validate(ref viewModel, ref appointment))
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CreateNew", viewModel);
            }


            
        }


        public ActionResult Delete(int id, DateTime dateToDisplay)
        {
            var appointment = _context.Appointments.SingleOrDefault(a => a.Id == id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            var DateTransfer = new AppointmentsListViewModel
            {
                DateToDisplay = dateToDisplay
            };

            return RedirectToAction("Index", DateTransfer);
        }
    }
}