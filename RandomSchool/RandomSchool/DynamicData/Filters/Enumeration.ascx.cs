using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Filters 
{
    public partial class EnumerationFilter : ScaffoldFilter.ScaffoldFilterUserControl 
	{
        public override Control FilterControl
        {
            get
            {
                return SFFilter_DropDownList1;
            }
        }

		public void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateListControl(SFFilter_DropDownList1);

				string defaultValue = "";
				FilterLoadEventArgs flea = new FilterLoadEventArgs();
				flea.FilterDefaults = null;
				this.OnFilterLoad(flea);

				if (flea.FilterDefaults != null)
				{
					flea.FilterDefaults.TryGetValue(Column.FieldName, out defaultValue);
					if (defaultValue != null) {
						SFFilter_DropDownList1.SelectedValue = defaultValue;
					}
				}
            }
        }

		protected void SFFilter_DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFilterChanged(SFFilter_DropDownList1.SelectedValue, Column.FieldName);
        }
    }
}
