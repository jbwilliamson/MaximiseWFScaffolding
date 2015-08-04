using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vCourse
{
    public partial class Delete : System.Web.UI.Page
    {
		private CourseRepositoryAsync<RandomSchoolAsync.Models.Course, int> _repository = new CourseRepositoryAsync<RandomSchoolAsync.Models.Course, int>();

		protected void Page_Init()
        {
            fvCourse.SetDataMethodsObject(_repository);
            fvCourse.RedirectToRouteOnItemDeleted("~/Maintain/vCourse/Default");
            fvCourse.RedirectToRouteOnItemCommand("~/Maintain/vCourse/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

