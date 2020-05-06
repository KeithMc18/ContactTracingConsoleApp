using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Data.Entity;

namespace ContactTracingApp.Models
{
    public class Person
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required]
        [Range(1000000, 999999999999999, ErrorMessage = "The Phone number must be at least 9 digits.")]
        public int Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [NotMapped]
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "The Email and Confirm Email must match")]
        public string ConfirmEmailAddress { get; set; }


        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{Dob:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Dob { get; set; }

    }


    public class PersonDBContext : DbContext
    {
        public PersonDBContext() : base("ContactTracingDB") { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            base.OnModelCreating(modelBuilder);
        }
    }
}