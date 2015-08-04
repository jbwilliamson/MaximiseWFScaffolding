using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;
using RandomSchoolAsync.FieldTemplates;
using System.Threading.Tasks;
using ScaffoldFilter;

namespace RandomSchoolAsync.Maintain.vPerson
{
    public partial class Insert : System.Web.UI.Page
    {
		private PersonRepositoryAsync<RandomSchoolAsync.Models.Person, int> _repository = new PersonRepositoryAsync<RandomSchoolAsync.Models.Person, int>();

		protected void Page_Init()
        {
            fvPerson.SetDataMethodsObject(_repository);
            fvPerson.RedirectToRouteOnItemInserted("~/Maintain/vPerson/Default");
            fvPerson.RedirectToRouteOnItemCommand("~/Maintain/vPerson/Default");
        }
        
		protected void Page_Load(object sender, EventArgs e)
        {

        }

		public async Task ForeignKeyEventHandler_LoadForeignKey(ForeignModelEventArgs e)
        {
            e.returnResults = await _repository.GetForeignListAsync(e.foreignKeyModel, e.keyType);
        }

        protected void dcForeignKey_Load(object sender, EventArgs e)
        {
            System.Web.DynamicData.DynamicControl dc = (System.Web.DynamicData.DynamicControl)sender;
            ForeignKey_EditField fkef = (ForeignKey_EditField)dc.FieldTemplate;
			
			if (fkef != null)
			{
				fkef.ForeignKey += new AsyncForeignKeyEventHandler(ForeignKeyEventHandler_LoadForeignKey);
			}
        }
    }
}
