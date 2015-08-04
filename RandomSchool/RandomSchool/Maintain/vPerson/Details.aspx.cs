using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vPerson
{
    public partial class Details : System.Web.UI.Page
    {
		private PersonRepository<RandomSchool.Models.Person, int?> _repository = new PersonRepository<RandomSchool.Models.Person, int?>();

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

