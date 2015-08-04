using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vGrade
{
    public partial class Details : System.Web.UI.Page
    {
		private GradeRepository<RandomSchool.Models.Grade, int?> _repository = new GradeRepository<RandomSchool.Models.Grade, int?>();

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

