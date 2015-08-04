using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vPerson
{
    public partial class Details : System.Web.UI.Page
    {
		private PersonRepositoryAsync<RandomSchoolAsync.Models.Person, int?> _repository = new PersonRepositoryAsync<RandomSchoolAsync.Models.Person, int?>();

		protected void Page_Init()
        {
            fvPerson.SetDataMethodsObject(_repository);
            fvPerson.RedirectToRouteOnItemCommand("~/Maintain/vPerson/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

