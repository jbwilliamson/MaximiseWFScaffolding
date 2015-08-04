using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchool.Models
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }

        [Display(Name = "School Type")]
        [Required(ErrorMessage = "The type of school is required")]
        [Description("The type of school")]
        public int SchoolTypeId { get; set; }

        [Display(Name = "School Name")]
        [Required(ErrorMessage = "The school name is required")]
        [Description("The name of the school")]
        public string SchoolName { get; set; }

        public virtual SchoolTypeCode SchoolType { get; set; }
    }
}