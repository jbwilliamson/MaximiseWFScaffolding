using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;

namespace RandomSchool.Maintain.vPupil
{
    public partial class Details : System.Web.UI.Page
    {
		private PupilRepository<RandomSchool.Models.Pupil, int?> _repository = new PupilRepository<RandomSchool.Models.Pupil, int?>();

		protected void Page_Init()
        {
            fvPupil.SetDataMethodsObject(_repository);
            fvPupil.RedirectToRouteOnItemCommand("~/Maintain/vPupil/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

