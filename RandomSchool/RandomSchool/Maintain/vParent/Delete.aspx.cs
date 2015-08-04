using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vParent
{
    public partial class Delete : System.Web.UI.Page
    {
		private ParentRepository<RandomSchool.Models.Parent, int> _repository = new ParentRepository<RandomSchool.Models.Parent, int>();

		protected void Page_Init()
        {
            fvParent.SetDataMethodsObject(_repository);
            fvParent.RedirectToRouteOnItemDeleted("~/Maintain/vParent/Default");
            fvParent.RedirectToRouteOnItemCommand("~/Maintain/vParent/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

