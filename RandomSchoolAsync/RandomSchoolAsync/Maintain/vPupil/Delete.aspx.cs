using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;

namespace RandomSchoolAsync.Maintain.vPupil
{
    public partial class Delete : System.Web.UI.Page
    {
		private PupilRepositoryAsync<RandomSchoolAsync.Models.Pupil, int> _repository = new PupilRepositoryAsync<RandomSchoolAsync.Models.Pupil, int>();

		protected void Page_Init()
        {
            fvPupil.SetDataMethodsObject(_repository);
            fvPupil.RedirectToRouteOnItemDeleted("~/Maintain/vPupil/Default");
            fvPupil.RedirectToRouteOnItemCommand("~/Maintain/vPupil/Default");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

