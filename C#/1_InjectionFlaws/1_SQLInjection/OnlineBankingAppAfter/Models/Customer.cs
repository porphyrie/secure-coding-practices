using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "Middle Name")]
        [StringLength(60, MinimumLength = 3)]
        public string MiddleName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [ReputableEmail]
        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(10)]
        [Required]
        public string Phone { get; set; }

        public List<Account> Accounts { get; set; }
    }
}