using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vGrade
{
    public partial class Details : System.Web.UI.Page
    {
		private GradeRepositoryAsync<RandomSchoolAsync.Models.Grade, int?> _repository = new GradeRepositoryAsync<RandomSchoolAsync.Models.Grade, int?>();

		protected void Page_Init()
        {
            fvGrade.SetDataMethodsObject(_repository);
            fvGrade.RedirectToRouteOnItemCommand("~/Maintain/vGrade/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

