using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchoolAsync.Models
{
    public class Grading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(1)]
        public string GradeLetter { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}