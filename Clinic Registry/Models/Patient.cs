using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Registry.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Pesel { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        [StringLength(9, MinimumLength = 9)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Date of registration")]
        public DateTime DateAddedToRegistry { get; set; }
        [Required]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public byte GenderId { get; set; }

        public Gender Gender { get; set; }
    }
}