using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchool.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}