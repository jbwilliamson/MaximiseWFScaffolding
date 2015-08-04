using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScaffoldFilter;

namespace RandomSchoolAsync.Models
{
    public enum disability { No = 0, Yes = 1, DoNotRecord = 2 }

    public class Pupil
    {
        [Key]
        [Display(Name = "Student Id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldFilter.GridView(true)]
        public int StudentId { get; set; }

        [Display(Name = "Person Id", Order = 2, GroupName = "Name", Description = "Select the person to match this student")]
        [Required(ErrorMessage = "Please select person to match this student")]
        [ScaffoldFilter.GridView(true)]
        [UIHint("ForeignKey", null, "RequireSorting", "Yes")]
        public int PersonId { get; set; }

        [Display(Name = "URN", Order = 3, Prompt="[NNNNNN]", GroupName = "Name")]
        [Description("Unique Reference Number [6 digits]")]
        [Required(ErrorMessage = "Unique Reference Number is a required field")]
        [MaxLength(6)]
        [RegularExpression(@"(\d{6})", ErrorMessage = "Unique Reference Number should be a 6 digit number only")]
        public string URN { get; set; }

        [Display(Name = "First Parent or Guardian", Order = 4, GroupName = "Name")]
        [Description("Select the pupil main Parent or Guardian")]
        [Required(ErrorMessage = "Please select a Parent or Guardian")]
        [UIHint("ForeignKey", null, "RequireSorting", "Yes")]
        public int ParentOne { get; set; }

        [Display(Name = "Second Parent or Guardian", Order = 5, GroupName = "Name")]
        [Description("Select the pupil secondary Parent or Guardian, optional")]
        [UIHint("ForeignKey", null, "RequireSorting", "Yes")]
        public int? ParentTwo { get; set; }

        [Display(Name = "Academic Year", Order = 6, GroupName = "Name")]
        [Description("Current School Year")]
        [Required(ErrorMessage = "The Academic Year is required for the pupil")]
        [StringLength(4)]
        [RegularExpression(@"(\d{4})", ErrorMessage = "Please enter a valid year")]
        [MaxLength(4)]
        public string AcademicYear { get; set; }

        [Display(Name = "Home Address Line 1", Order = 7, Prompt = "Address 1", GroupName = "Address", Description = "Home Address Line 1")]
        [Required(ErrorMessage = "A home address is required")]
        [MaxLength(100)]
        public string HomeAddress1 { get; set; }

        [Display(Name = "Home Address Line 2", Order = 8, Prompt = "Address 2", GroupName = "Address", Description = "Home Address Line 2")]
        [MaxLength(100)]
        public string HomeAddress2 { get; set; }

        [Display(Name = "Town or City", Order = 9, Prompt = "Town or City Name", GroupName = "Address")]
        [Description("Home Address Town or City")]
        [Required(ErrorMessage = "Town or City is required")]
        [MaxLength(100)]
        [ScaffoldFilter.GridView(true)]
        public string Town { get; set; }

        [Display(Name = "County", Order = 10, Prompt = "County Name", GroupName = "Address")]
        [Description("Home Address County")]
        [Required(ErrorMessage = "County is required")]
        [MaxLength(100)]
        public string County { get; set; }

        [Display(Name = "Country", Order = 11, Prompt = "Country Name", GroupName = "Address")]
        [Description("Home Address Country")]
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Display(Name = "Postcode", Order = 12, GroupName = "Address")]
        [Description("Home Postcode")]
        [Required(ErrorMessage = "A valid uk postcal code is required")]
        [RegularExpression("[A-Za-z]{1,2}[0-9Rr][0-9A-Za-z]? [0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}", ErrorMessage = "Please enter a valid UK postal code")]
        [MaxLength(15)]
        public string Postcode { get; set; }

        [Display(Name = "Home Phone", Order = 13, Prompt = "Home", GroupName = "Phone")]
        [Description("Home Phone Number")]
        [Required(ErrorMessage = "Please enter a valid uk phone number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk phone number")]
        [MaxLength(50)]
        public string HomePhone { get; set; }

        [Display(Name = "Mobile Phone", Order = 14, Prompt = "Mobile", GroupName = "Phone")]
        [Description("Mobile Phone Number")]
        [Required(ErrorMessage = "The students mobile phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Phone Number")]
        [RegularExpression(@"//^\s*\(?(020[7,8]{1}\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\s*$", ErrorMessage = "Please enter a valid uk mobile phone number")]
        [MaxLength(50)]
        public string MobilePhone { get; set; }

        [Display(Name = "Age", Order = 15, Prompt = "##", GroupName = "Personal")]
        [Description("Current age of the student")]
        [Required(ErrorMessage = "The age of the student is required")]
        [Range(10, 100, ErrorMessage = "Age should a numeric value between 10 and 100")]
        public int Age { get; set; }

        [Display(Name = "Gender", Order = 16, GroupName = "Personal")]
        [Description("Gender of Student")]
        [Required(ErrorMessage = "Please select the students gender")]
        [ScaffoldFilter.GridView(true)]
        [FilterUIHint("Enumeration")]
        public egender Gender { get; set; }

        [Display(Name = "Email Address", Order = 20, Prompt = "yourname@randomschoolasync.co.uk", GroupName = "Contact")]
        [Description("yourname@randomschoolasync.co.uk")]
        [Required(ErrorMessage = "A valid work email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [Display(Name = "Twitter", Order = 21, Prompt = "@twitteraccount", GroupName = "Contact")]
        [Description("Personal Twitter Account")]
        [RegularExpression(@"^@?(\w){1,15}$", ErrorMessage = "Please enter a valid twitter account name, blank if none")]
        [MaxLength(100)]
        public string Twitter { get; set; }

        [Display(Name = "Instagram", Order = 22, Prompt = "@instagram", GroupName = "Contact")]
        [Description("Personal Instagram Account")]
        [RegularExpression(@"^@?(\w){1,15}$", ErrorMessage = "Please enter a valid instagram account name, blank if none")]
        [MaxLength(100)]
        public string Instagram { get; set; }

        [Display(Name = "Facebook Page", Order = 23, GroupName = "Contact")]
        [Description("Students facebook profile page or blank if none")]
        [MaxLength(200)]
        public string FacebookPage { get; set; }

        [Display(Name = "Student disability", Order = 17, GroupName = "Personal")]
        [Description("Any student disability, optional")]
        [FilterUIHint("Enumeration")]
        [DefaultValue(0)]
        public disability Disability { get; set; }

        [Display(Name = "Nationality", Order = 18, GroupName = "Personal")]
        [Description("Nationality of student")]
        [Required(ErrorMessage = "The students nationality is required")]
        [ScaffoldFilter.GridView(true)]
        [FilterUIHint("ForeignKey")]
        [UIHint("ForeignKey", null, "RequireSorting", "Yes")]
        public int NationId { get; set; }

        [Display(Name = "Date Of Birth", Order = 19, Prompt = "Date: yyyy-mm-dd", GroupName = "Personal")]
        [Description("Student's Date Of Birth")]
        [Required(ErrorMessage = "The students date of birth is requried")]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date entered")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ScaffoldFilter.GridView(true)]
        [ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside storage range")]
        [Range(typeof(DateTime), "1900-01-01", "2015-01-01", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Other Information", Order = 24, Prompt = "Student details", GroupName = "Additional")]
        [Description("Record any other information regarding the student believed relevate")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255)]
        public string OtherInformation { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect date entered")]
        [Display(Name = "Enrollment Date", Order = 25, GroupName = "Additional")]
        [Required(ErrorMessage = "Date student was enrolled is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "2000-01-01 00:00", "2100-01-01 00:00", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [ScaffoldFilter.GridView(true)]
        [ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside acceptable range")]
        public DateTime EnrollmentDate { get; set; }


        public virtual Person Person { get; set; }
        [ForeignKey("NationId")]
        public virtual Nation Nation { get; set; }
        
        [ForeignKey("ParentOne")]
        public virtual Parent FirstParent { get; set; }
        [ForeignKey("ParentTwo")]
        public virtual Parent SecondParent { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
