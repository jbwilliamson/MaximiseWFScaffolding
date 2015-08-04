using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vTeacher
{
    public partial class Details : System.Web.UI.Page
    {
		private TeacherRepository<RandomSchool.Models.Teacher, int?> _repository = new TeacherRepository<RandomSchool.Models.Teacher, int?>();

		protected void Page_Init()
        {
            fvTeacher.SetDataMethodsObject(_repository);
            fvTeacher.RedirectToRouteOnItemCommand("~/Maintain/vTeacher/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

