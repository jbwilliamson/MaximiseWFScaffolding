using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchoolAsync.Models
{
    public class Teacher
    {
        [Key]
        [Display(Name = "Teacher Id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldFilter.GridView(true)]
        public int TeacherId { get; set; }

        [Display(Name = "Person Id", Order = 2, GroupName = "Name", Description = "Select the person to match this Instrutorxxx")]
        [Required(ErrorMessage = "Please select person to match this Instrutor")]
        [ScaffoldFilter.GridView(true)]
        [UIHint("ForeignKey", null, "RequireSorting", "Yes")]
        public int PersonId { get; set; }

        [Display(Name = "Home Address Line 1", Order = 3, Prompt = "Address 1", GroupName = "Address", Description = "Home Address Line 1")]
        [Required(ErrorMessage = "A home address is required")]
        [MaxLength(100)]
        public string HomeAddress1 { get; set; }

        [Display(Name = "Home Address Line 2", Order = 4, Prompt = "Address 2", GroupName = "Address", Description = "Home Address Line 2")]
        [MaxLength(100)]
        public string HomeAddress2 { get; set; }

        [Display(Name = "Town or City", Order = 5, Prompt = "Town or City Name", GroupName = "Address")]
        [Description("Home Address Town or City")]
        [Required(ErrorMessage = "Town or City is required")]
        [MaxLength(100)]
        [ScaffoldFilter.GridView(true)]
        public string Town { get; set; }

        [Display(Name = "County", Order = 6, Prompt = "County Name", GroupName = "Address")]
        [Description("Home Address County")]
        [Required(ErrorMessage = "County is required")]
        [MaxLength(100)]
        public string County { get; set; }

        [Display(Name = "Country", Order = 7, Prompt = "Country Name", GroupName = "Address")]
        [Description("Home Address Country")]
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Display(Name = "Postcode", Order = 8, GroupName = "Address")]
        [Description("Home Postcode")]
        [Required(ErrorMessage = "A valid uk postcal code is required")]
        [RegularExpression("[A-Za-z]{1,2}[0-9Rr][0-9A-Za-z]? [0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}", ErrorMessage = "Please enter a valid UK postal code")]
        [MaxLength(15)]
        public string Postcode { get; set; }

        [Display(Name = "Home Phone", Order = 9, Prompt = "Home", GroupName = "Phone")]
        [Description("Home Phone Number")]
        [Required(ErrorMessage = "Please enter a valid uk phone number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk phone number")]
        [MaxLength(50)]
        public string HomePhone { get; set; }

        [Display(Name = "Mobile Phone", Order = 10, Prompt = "Mobile", GroupName = "Phone")]
        [Description("Mobile Phone Number")]
        [Required(ErrorMessage = "The instructors mobile phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk mobile phone number")]
        [MaxLength(50)]
        public string MobilePhone { get; set; }

        [Display(Name = "Age", Order = 11, Prompt = "##", GroupName = "Personal")]
        [Description("Current age of Instructor")]
        [Required(ErrorMessage = "The age of the instructors is required")]
        [Range(10, 100, ErrorMessage = "Age should a numeric value between 10 and 100")]
        public int Age { get; set; }

        [Display(Name = "Gender", Order = 12, GroupName = "Personal")]
        [Description("Gender of Instructor")]
        [Required(ErrorMessage = "Please select the instructors gender")]
        [ScaffoldFilter.GridView(true)]
        [FilterUIHint("Enumeration")]
        public egender Gender { get; set; }

        [Display(Name = "Salary", Order = 15, Prompt = "######.##", GroupName = "Personal")]
        [Description("Instructor's Current Salary")]
        [Required(ErrorMessage = "The instructors salary is required")]
        [Range(10.00, 999999.00, ErrorMessage = "Salary should contain a numeric money value [######.##]")]
        public decimal Salary { get; set; }

        [Display(Name = "Email Address", Order = 16, Prompt = "yourname@exampleschool.co.uk", GroupName = "Contact")]
        [Description("yourname@exampleschool.co.uk")]
        [Required(ErrorMessage = "A valid work email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [Display(Name = "Your Website", Order = 17, GroupName = "Contact")]
        [DefaultValue("https://")]
        [Description("Your Work Website")]
        [Required(ErrorMessage = "Your webside link is required")]
        [DataType(DataType.Url, ErrorMessage = "Invalid URL Address")]
        [MaxLength(200)]
        public string WebSite { get; set; }

        [Display(Name = "Twitter", Order = 18, Prompt = "@twitteraccount", GroupName = "Contact")]
        [Description("Personal Twitter Account")]
        [RegularExpression(@"^@?(\w){1,15}$", ErrorMessage = "Please enter a valid twitter account name, blank if none")]
        public string Twitter { get; set; }

        [Display(Name = "Nationality", Order = 13, GroupName = "Personal")]
        [Description("Instructor's Nationality")]
        [Required(ErrorMessage = "The instructors nationality is required")]
        [FilterUIHint("ForeignKey")]
        [UIHint("ForeignKey", null, "RequireSorting", "Yes")]
        public int NationId { get; set; }

        [Display(Name = "Date Of Birth", Order = 14, Prompt = "Date: yyyy-mm-dd", GroupName = "Personal")]
        [Description("Instructor's Date Of Birth")]
        [Required(ErrorMessage = "The instructors date of birth is requried")]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date entered")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside acceptable range")]
        [Range(typeof(DateTime), "1900-01-01", "2015-01-01", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Academic Information", Order = 19, Prompt = "Academic details", GroupName = "Additional")]
        [Description("Instructor's Academic Information")]
        [Required(ErrorMessage = "Academic information is required for the records")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255)]
        public string AcademicInformation { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date", Order = 20, Prompt = "Date: yyyy-MM-dd hh:mm", GroupName = "Additional")]
        [Description("Date Instructor was hired")]
        [ScaffoldFilter.GridView(true)]
        [ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside acceptable range")]
        [Range(typeof(DateTime), "2000-01-01 00:00", "2100-01-01 00:00", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfHire { get; set; }

        [StringLength(100, MinimumLength = 10, ErrorMessage = "Minimum length of location text is 10 characters")]
        [Display(Name = "Office Location", Order = 21, Prompt = "Location [10 char min]", GroupName = "Additional")]
        [Description("Instructor assigned office location")]
        public string Location { get; set; }

        public virtual Person Person { get; set; }
        [ForeignKey("NationId")]
        public virtual Nation Nation { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}