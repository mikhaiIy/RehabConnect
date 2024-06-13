using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Parent
    {
        [Key]
        public int ParentID { get; set; }

        [Required]
        public string FatherName { get; set; }
        [Required]
        public string FatherPhoneNum { get; set; }
        [Required]
        public string FatherIC { get; set; }
        [Required]
        public string FatherRace { get; set; }
        [Required]
        public string FatherOccupation { get; set; }
        [Required]
        public string FatherEmail { get; set; }
        [Required]
        public string FatherAddress { get; set; }
        [Required]
        public string FatherPostcode { get; set; }
        [Required]
        public string FatherCity { get; set; }
        [Required]
        public string FatherCountry { get; set; }
        public string? FatherWorkAddress { get; set; }


        [Required]
        public string MotherName { get; set; }
        [Required]
        public string MotherPhoneNum { get; set; }
        [Required]
        public string MotherIC { get; set; }
        [Required]
        public string MotherRace { get; set; }
        [Required]
        public string MotherOccupation { get; set; }
        [Required]
        public string MotherEmail { get; set; }
        [Required]
        public string MotherAddress { get; set; }
        [Required]
        public string MotherPostcode { get; set; }
        [Required]
        public string MotherCity { get; set; }
        [Required]
        public string MotherCountry { get; set; }
        public string? MotherWorkAddress { get; set; }


        [Required]
        public string HouseholdIncome { get; set; }

        // Navigation properties
        public ICollection<Billing> Billings { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
