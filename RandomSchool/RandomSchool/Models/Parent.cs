using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchool.Models
{
    public enum eMaritalStatus { Married = 1, Widowed = 2, Separated = 3, Divorced = 4, Single = 5};
    public enum egender { Male = 1, Female = 2 }

    public class Parent
    {
        [Key]
        [Display(Name = "Parent Id", Order = 1, GroupName="Identity")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldFilter.GridView(true)]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        [Display(Name = "First Name", Order = 2, Prompt = "First name", GroupName="Identity")]
        [Description("Parents first name")]
        [MaxLength(100)]
        [ScaffoldFilter.GridView(true)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The surname is required")]
        [Display(Name = "Surname", Order = 3, Prompt = "Surname", GroupName = "Identity")]
        [Description("Parents Surname")]
        [MaxLength(100)]
        [ScaffoldFilter.GridView(true)]
        public string Surname { get; set; }

        [ScaffoldFilter.GridView(true)]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + Surname;
            }
        }

        [Display(Name = "Email Address", Order = 4, Prompt = "yourname@domain.co.uk", GroupName = "Contact")]
        [Description("yourname@domain.co.uk")]
        [Required(ErrorMessage = "A valid Personal email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [MaxLength(200)]
        [ScaffoldFilter.GridView(true)]
        public string EmailAddress { get; set; }
        
        [Display(Name = "Home Address Line 1", Order = 5, Prompt = "Address 1", GroupName = "Address", Description = "Home Address Line 1")]
        [Required(ErrorMessage = "A home address is required")]
        [MaxLength(100)]
        public string HomeAddress1 { get; set; }

        [Display(Name = "Home Address Line 2", Order = 6, Prompt = "Address 2", GroupName = "Address", Description = "Home Address Line 2")]
        [MaxLength(100)]
        public string HomeAddress2 { get; set; }

        [Display(Name = "Town or City", Order = 7, Prompt = "Town or City Name", GroupName = "Address")]
        [Description("Home Address Town or City")]
        [Required(ErrorMessage = "Town or City is required")]
        [MaxLength(100)]
        [ScaffoldFilter.GridView(true)]
        public string Town { get; set; }

        [Display(Name = "County", Order = 8, Prompt = "County Name", GroupName = "Address")]
        [Description("Home Address County")]
        [Required(ErrorMessage = "County is required")]
        [MaxLength(100)]
        public string County { get; set; }

        [Display(Name = "Country", Order = 9, Prompt = "Country Name", GroupName = "Address")]
        [Description("Home Address Country")]
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Display(Name = "Postcode", Order = 10, GroupName = "Address")]
        [Description("Home Postcode")]
        [Required(ErrorMessage = "A valid uk postcal code is required")]
        [RegularExpression("[A-Za-z]{1,2}[0-9Rr][0-9A-Za-z]? [0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}", ErrorMessage = "Please enter a valid UK postal code")]
        [MaxLength(15)]
        public string Postcode { get; set; }

        [Display(Name = "Home Phone", Order = 11, Prompt = "Home", GroupName = "Phone")]
        [Description("Home Phone Number")]
        [Required(ErrorMessage = "The parents home phone number number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk phone number")]
        [MaxLength(50)]
        public string HomePhone { get; set; }

        [Display(Name = "Mobile Phone", Order = 12, Prompt = "Mobile", GroupName = "Phone")]
        [Description("Mobile Phone Number")]
        [Required(ErrorMessage = "The parents mobile phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk mobile phone number")]
        [MaxLength(50)]
        public string MobilePhone { get; set; }

        [Display(Name = "Work Phone", Order = 13, Prompt = "Work", GroupName = "Phone")]
        [Description("Work Phone Number")]
        [Required(ErrorMessage = "The parents work phone number number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk phone number")]
        [MaxLength(50)]
        public string WorkPhone { get; set; }

        [Display(Name = "Date Of Birth", Order = 14, Prompt = "Date: yyyy-mm-dd", GroupName = "Personal")]
        [Description("Parents Date Of Birth")]
        [Required(ErrorMessage = "The parents date of birth is requried")]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date entered")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ScaffoldFilter.GridView(true)]
        [ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside storage range")]
        [Range(typeof(DateTime), "1900-01-01", "2015-01-01", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DOB { get; set; }

        [Display(Name = "Gender", Order = 15, GroupName = "Personal")]
        [Description("Gender of Instructor")]
        [Required(ErrorMessage = "Please select the parents gender")]
        [ScaffoldFilter.GridView(true)]
        [FilterUIHint("Enumeration")]
        public egender Gender { get; set; }

        [Display(Name = "Marital Status", Order = 16, GroupName = "Personal")]
        [Description("Marital Status of parent")]
        [Required(ErrorMessage = "Please select the parents Marital Status")]
        [ScaffoldFilter.GridView(true)]
        [FilterUIHint("Enumeration")]
        public eMaritalStatus Status {get; set;}

        [Display(Name = "Job Description", Order = 17, Prompt = "Job", GroupName = "Job")]
        [Description("Description of parents employment, optional")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255)]
        public string JobDescription { get; set; }

        public virtual ICollection<Pupil> Pupil { get; set; }
    }
}