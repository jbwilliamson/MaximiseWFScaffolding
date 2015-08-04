using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchoolAsync.Models
{
    public class Nation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}