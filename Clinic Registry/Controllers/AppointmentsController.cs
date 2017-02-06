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
        private AppointmentsListViewModel appointmentsViewModel;


        public AppointmentsController()
        {
            _context = new ApplicationDbContext();
            appointmentsViewModel = new AppointmentsListViewModel
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
        public ActionResult Index(AppointmentsDateTransfer appointmentsDateTransfer)
        {  
            //Checking for a date
            if(appointmentsDateTransfer.Date == DateTime.MinValue)
            {
                appointmentsViewModel.Date = DateTime.Today;
            }
            else
            {
                appointmentsViewModel.Date = appointmentsDateTransfer.Date;
            }

            //Choosing appointments with the valid date
            appointmentsViewModel.TempAppointmentsList.Clear();
            foreach (var appointment in appointmentsViewModel.AppointmentsList)
            {
                if (appointment.Date == appointmentsViewModel.Date)
                    appointmentsViewModel.TempAppointmentsList.Add(appointment);
            }

            foreach (var appointment in appointmentsViewModel.TempAppointmentsList)
            {
                appointment.Doctor = _context.Doctors.SingleOrDefault(d => d.Id == appointment.DoctorId);
                appointment.Patient = _context.Patients.SingleOrDefault(p => p.Id == appointment.PatientId);
            }

            return View("AppointmentsList", appointmentsViewModel);
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


        public ActionResult Delete(int id)
        {
            var appointment = _context.Appointments.SingleOrDefault(a => a.Id == id);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}