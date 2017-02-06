using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Mvc;
using Clinic_Registry.Models;
using Clinic_Registry.ViewModels;
using AutoMapper;

namespace Clinic_Registry.Controllers
{
    public class PatientsController : Controller
    {


        private ApplicationDbContext _context;
        private PatientsListViewModel patients;


        public PatientsController()
        {
            _context = new ApplicationDbContext();
            patients = new PatientsListViewModel
            {
                PatientsList = _context.Patients.Include(p => p.Gender).ToList()
            };
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }




        //Action supports displaying the patients list
        public ActionResult Index()
        {

            return View("PatientsList", patients);

        }


        //Action supports displaying the user details
        public ActionResult Details(int id)
        {


            var patient = patients.PatientsList.SingleOrDefault(p => p.Id == id);
            if (patient != null)
                return View("PatientDetails", patient);


            return HttpNotFound();

        }


        //Action supports displaying the form for editing existing patient
        public ActionResult Edit(int id)
        {
            var patient = patients.PatientsList.SingleOrDefault(p => p.Id == id);

            if (patient != null)
            {
                var genders = _context.Genders.ToList();
                var viewModel = new PatientFormViewModel
                {
                    //sending genders to viewModel serves for filling the dropdown list
                    Genders = genders,            
                    Patient = patient
                };
                return View("PatientForm", viewModel);
            }


            return HttpNotFound();



        }


        //Action supports displaying the form for create new patient
        public ActionResult CreateNew()
        {
            var genders = _context.Genders.ToList();
            var viewModel = new PatientFormViewModel
            {
                //sending genders to viewModel serves for filling the dropdown list
                Genders = genders,
                //sending initiated Patient serves for filling the hidden input
                Patient = new Patient
                {
                    DateAddedToRegistry = DateTime.Now

                }
        
            };

            return View("PatientForm", viewModel);
        }


        //Action saves Patient to Database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PatientFormViewModel()
                {
                    Patient = patient,
                    Genders = _context.Genders.ToList()
                };


                return View("PatientForm", viewModel);
            }


            //This occurs when patient comes from CreateNew() action
            if (patient.Id == 0)
                _context.Patients.Add(patient);
            //This occurs when patient comes from Edit() Action
            else
            {
                var patientInDB = _context.Patients.Single(p => p.Id == patient.Id);

                Mapper.Map(patient, patientInDB);

                //Property Gender need to be assigned in order to not being a null
                patientInDB.Gender = _context.Genders.SingleOrDefault(g => g.Id == patient.GenderId);
            }


            //Finally save changes
            _context.SaveChanges();

            return RedirectToAction("Index", "Patients");
        }

        
    }
}