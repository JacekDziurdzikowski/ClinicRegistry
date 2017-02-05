using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinic_Registry.Models;
using Clinic_Registry.ViewModels;
using AutoMapper;
using System.Data.Entity;

namespace Clinic_Registry.Controllers
{
    public class DoctorsController : Controller
    {
        private ApplicationDbContext _context;
        private DoctorsListViewModel doctors;


        public DoctorsController()
        {
            _context = new ApplicationDbContext();
            doctors = new DoctorsListViewModel
            {
                DoctorsList = _context.Doctors.Include(d => d.Specialization).ToList()
            };
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }




        //Action supports displaying the users list
        public ActionResult Index()
        {
            //doctors.DoctorsList.Select(d => d.Specialization = _context.Specializations.SingleOrDefault(s => s.Id == d.SpecializationId));
            foreach (var doctor in doctors.DoctorsList)
            {
                doctor.Specialization = _context.Specializations.SingleOrDefault(s => s.Id == doctor.SpecializationId);
            }

            return View("DoctorsList", doctors);
        }


        //Action supports displaying the user details
        public ActionResult Details(int id)
        {
            var doctor = doctors.DoctorsList.SingleOrDefault(p => p.Id == id);
            doctor.Specialization = _context.Specializations.SingleOrDefault(s => s.Id == doctor.SpecializationId);
            if (doctor != null)
                return View("DoctorDetails", doctor);


            return HttpNotFound();
        }


        //Action supports displaying the form for editing existing doctor
        public ActionResult Edit(int id)
        {
            var doctor = doctors.DoctorsList.SingleOrDefault(p => p.Id == id);

            if (doctor != null)
            {
                var specializations = _context.Specializations.ToList();
                var viewModel = new DoctorFormViewModel
                {
                    //sending specializations and doctor to viewModel serves for filling the dropdown list
                    Specializations = specializations,
                    Doctor = doctor
                };
                return View("DoctorForm", viewModel);
            }


            return HttpNotFound();
        }


        //Action supports displaying the form for create new doctor
        public ActionResult CreateNew()
        {
            var specializations = _context.Specializations.ToList();
            var viewModel = new DoctorFormViewModel
            {
                //sending specializations to viewModel serves for filling the dropdown list
                Specializations = specializations,
                //sending initiated Doctor serves for filling the hidden input
                Doctor = new Doctor()

            };

            return View("DoctorForm", viewModel);
        }


        //Action saves Doctor to Database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new DoctorFormViewModel()
                {
                    Doctor = doctor,
                    Specializations = _context.Specializations.ToList()
                };


                return View("DoctorForm", viewModel);
            }


            //This occurs when doctor comes from CreateNew() action
            if (doctor.Id == 0)
                _context.Doctors.Add(doctor);
            //This occurs when doctor comes from Edit() Action
            else
            {
                var doctorInDB = _context.Doctors.Single(p => p.Id == doctor.Id);

                Mapper.Map(doctor, doctorInDB);

                //Property Specialization need to be assigned in order to not being a null
                doctorInDB.Specialization = _context.Specializations.SingleOrDefault(g => g.Id == doctor.SpecializationId);
            }


            //Finally save changes
            _context.SaveChanges();

            return RedirectToAction("Index", "Doctors");
        }
    }
}