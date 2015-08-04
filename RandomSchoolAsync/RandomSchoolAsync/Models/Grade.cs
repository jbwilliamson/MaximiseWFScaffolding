using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchoolAsync.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id", Order = 1, ShortName = "Id")]
        public int id { get; set; }

        [Display(Name = "Year", Order = 2, ShortName = "Year", GroupName = "Grade")]
        [Description("The school year the grade refers to")]
        [Required(ErrorMessage = "The school year is required")]
        public int YearId { get; set; }

        [Display(Name = "Grade", Order = 3, ShortName = "Grade", GroupName = "Grade")]
        [Description("Specify the pupils grade")]
        [Required(ErrorMessage = "The pupil grade is required")]
        public int GradeId { get; set; }

        [Display(Name = "Course", Order = 4, ShortName = "Course", GroupName = "Grade")]
        [Description("Specify the pupils course")]
        [Required(ErrorMessage = "The pupil course is required")]
        public int CourseId { get; set; }

        [Display(Name = "Pupil", Order = 5, ShortName = "Pupil", GroupName = "Grade")]
        [Description("Specify the pupil being garded")]
        [Required(ErrorMessage = "The pupil is required")]
        public int PupilId { get; set; }

        [ForeignKey("YearId")]
        public virtual Year Year { get; set; }
        
        [ForeignKey("GradeId")]
        public virtual Grading Grading { get; set; }
        public virtual Course Course { get; set; }
        
        [ForeignKey("PupilId")]
        public virtual Pupil Pupil { get; set; }
    }
}