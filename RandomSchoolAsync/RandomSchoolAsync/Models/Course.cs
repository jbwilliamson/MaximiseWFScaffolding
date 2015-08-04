using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomSchoolAsync.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Class Id", Order = 1, ShortName = "Id")]
        public int CourseId { get; set; }

        [Display(Name = "QAN", Order = 1, Prompt = "[nnn-nnnn-x]", ShortName = "QAN", GroupName = "Title")]
        [Required(ErrorMessage = "Qualification Accreditation Number is a required field")]
        [RegularExpression(@"(\d{3}-\d{4}-[A-Za-z0-9])", ErrorMessage = "QAN should consist of the format [NNN-NNNN-X]")]
        [Description("Specifies the Qualification Accreditation Number of the course, format: [NNN-NNNN-X]")]
        [MaxLength(10)]
        public string QAN { get; set; }

        [Display(Name = "Title", Order = 2, Prompt = "[Class Title]", GroupName = "Title")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A Course Title should contain between 3 and 50 characters")]
        [Required(ErrorMessage = "Class Title is a required field")]
        [Description("Specifies the title of the Class, should containing alpha-numeric characters only")]
        [RegularExpression("([a-zA-Z0-9 .&%*-]+)", ErrorMessage = "Class Title should consist of alpha-numeric characters only")]
        public string Title { get; set; }

        [Display(Name = "Description", Order = 3, Prompt = "[Class Description]", GroupName = "Description")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "A Class Description should contain between 3 and 200 characters")]
        [Required(ErrorMessage = "Class Description is a required field")]
        [Description("Specifies a Description of the class")]
        public string Description { get; set; }

        [Display(Name = "Code", Order = 4, Prompt = "[XNNN]", GroupName = "Description")]
        [Required(ErrorMessage = "Subject code is a required field")]
        [Description("Specifies the subject code for this class")]
        [RegularExpression(@"([a-zA-Z]\d{3})", ErrorMessage = "The subject code is a 4 alphanumeric code in the format [XNNN]")]
        [MaxLength(4)]
        public string SubjectCode { get; set; }

        [Display(Name = "Room", Order = 5, ShortName = "Room#", GroupName="Location")]
        [Description("The room designated to this class")]
        [Required(ErrorMessage = "The room number if a required field")]
        [FilterUIHint("ForeignKey", null, "TextField", "Description", "RequireSorting", "No")]
        public int RoomId { get; set; }

        [Display(Name = "Department", Order = 6, ShortName = "Dept#", GroupName="Location")]
        [Description("Course is part of this department")]
        [Required(ErrorMessage = "The course department is a required field")]
        [UIHint("ForeignKey", null, "TextField", "Name", "RequireSorting", "Yes")]
        [FilterUIHint("ForeignKey", null, "TextField", "Name", "RequireSorting", "Yes")]
        public int DepartmentId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
    }
}