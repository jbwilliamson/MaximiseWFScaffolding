using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vTeacher
{
    public partial class Details : System.Web.UI.Page
    {
		private TeacherRepositoryAsync<RandomSchoolAsync.Models.Teacher, int?> _repository = new TeacherRepositoryAsync<RandomSchoolAsync.Models.Teacher, int?>();

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

