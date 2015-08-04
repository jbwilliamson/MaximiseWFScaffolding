using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchool.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Person Id", Order = 1)]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "First Name", Order = 2, GroupName = "Person")]
        [Description("Student or instructors fist name")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "The last name is required")]
        [StringLength(50)]
        [Display(Name = "Last Name", Order = 3, GroupName = "Person")]
        [Description("Student or instructors last name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name", Order = 4)]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        [Display(Name = "School", Order=5, GroupName = "School")]
        [DisplayFormat(HtmlEncode=true)]
        [Required(ErrorMessage = "The school identity is required")]
        [Description("Select the school attended by this person")]
        [DefaultValue(1)]
        [FilterUIHint("ForeignKey")]
        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public override string ToString()
        {
            return LastName + ", " + FirstMidName;
        }
    }
}