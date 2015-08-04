using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchool.Models
{
    public class Year
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string SchoolYear { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
    }
}