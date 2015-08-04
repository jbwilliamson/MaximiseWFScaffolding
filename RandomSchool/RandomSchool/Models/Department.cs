using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchool.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Department Id", Order = 1, ShortName = "Id")]
        public int DepartmentId { get; set; }

        [Display(Name = "Department Name", Order = 2, ShortName = "Name", Prompt = "Name of department", GroupName = "Name")]
        [Description("The name given to this school department")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Minimum length of the department name is 3 characters")]
        [Required(ErrorMessage = "A department name is required")]
        [RegularExpression("([a-zA-Z0-9 .&%*-]+)", ErrorMessage = "Enter only alphanumeric characters for the Department Name")]
        public string Name { get; set; }

        [Display(Name = "Department Budget", Order = 3, ShortName = "Budget", Prompt = "######.##", GroupName = "Details")]
        [Description("The department yearly budget")]
        [Range(1000.00, 999999.99, ErrorMessage = "The entered budget should be numeric between 1000.00 and 999999.99")]
        [DisplayFormat(DataFormatString = "{0:###,###.##}")]
        public decimal Budget { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date", Order = 4, Prompt = "Date: yyyy-MM-dd", GroupName = "Details")]
        [Required(ErrorMessage = "A start date is required for the department")]
        [ScaffoldFilter.SQLDateAttribute(ErrorMessage = "{0} date specified is outside acceptable range")]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date entered")]
        [Range(typeof(DateTime), "2000-01-01", "2100-01-01", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Administrator", Order = 5, ShortName = "Administrator", GroupName = "Details")]
        [Description("The assigned department administrator")]
        public int? InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public virtual Teacher Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}