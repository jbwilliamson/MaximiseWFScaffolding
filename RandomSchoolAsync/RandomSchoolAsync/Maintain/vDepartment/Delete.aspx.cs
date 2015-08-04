using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vDepartment
{
    public partial class Delete : System.Web.UI.Page
    {
		private DepartmentRepositoryAsync<RandomSchoolAsync.Models.Department, int> _repository = new DepartmentRepositoryAsync<RandomSchoolAsync.Models.Department, int>();

		protected void Page_Init()
        {
            fvDepartment.SetDataMethodsObject(_repository);
            fvDepartment.RedirectToRouteOnItemDeleted("~/Maintain/vDepartment/Default");
            fvDepartment.RedirectToRouteOnItemCommand("~/Maintain/vDepartment/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

