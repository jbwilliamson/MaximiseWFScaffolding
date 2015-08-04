using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vParent
{
    public partial class Details : System.Web.UI.Page
    {
		private ParentRepositoryAsync<RandomSchoolAsync.Models.Parent, int?> _repository = new ParentRepositoryAsync<RandomSchoolAsync.Models.Parent, int?>();

		protected void Page_Init()
        {
            fvParent.SetDataMethodsObject(_repository);
            fvParent.RedirectToRouteOnItemCommand("~/Maintain/vParent/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

