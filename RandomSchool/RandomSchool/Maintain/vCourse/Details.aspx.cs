using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vCourse
{
    public partial class Details : System.Web.UI.Page
    {
		private CourseRepository<RandomSchool.Models.Course, int?> _repository = new CourseRepository<RandomSchool.Models.Course, int?>();

		protected void Page_Init()
        {
            fvCourse.SetDataMethodsObject(_repository);
            fvCourse.RedirectToRouteOnItemCommand("~/Maintain/vCourse/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

