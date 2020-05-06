using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactTracingApp.Models
{
    public class Contact
    {
        // Add Unit test

        // This should be generated when its submitted 
        [Key]
        public int Id { get; set; }

        public int ContactId { get; set; }


        // Add a date picker field 
        [Display(Name = "Date you last met")]
        public DateTime DateMet { get; set; }

        public int PersonId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter a vaild mobile number")]
        public string Mobile { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        // Add validation for negative for both below
        [Display(Name = "Distance Apart (M)")]
        public int DistanceKept { get; set; }

        [Display(Name = "Time Spent Together")]
        public int TimeSpent { get; set; }

    }
}