using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vDepartment
{
    public partial class Details : System.Web.UI.Page
    {
		private DepartmentRepository<RandomSchool.Models.Department, int?> _repository = new DepartmentRepository<RandomSchool.Models.Department, int?>();

		protected void Page_Init()
        {
            fvDepartment.SetDataMethodsObject(_repository);
            fvDepartment.RedirectToRouteOnItemCommand("~/Maintain/vDepartment/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

