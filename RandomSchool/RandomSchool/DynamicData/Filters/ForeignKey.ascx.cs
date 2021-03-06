﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Filters 
{
    public partial class ForeignKeyFilter : ScaffoldFilter.ScaffoldFilterUserControl 
	{
		public event ForeignKeyEventHandler ForeignKey; 

		public override Control FilterControl
        {
            get { return this.SFFilter_DropDownList1; }
        }

		protected void SFFilter_DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
			this.OnFilterChanged(SFFilter_DropDownList1.SelectedValue, Column.FieldName);
        }

        protected void SFFilter_DropDownList1_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ForeignModelEventArgs fmea = new ForeignModelEventArgs();
                fmea.returnResults = null;
                fmea.foreignKeyModel = Column.FieldName;
                fmea.keyType = ForeignModelEventArgs.LoadForeignTableByKey;

                if (this.ForeignKey != null) 
                {
                    SFFilter_DropDownList1.Items.Clear();
                    SFFilter_DropDownList1.Items.Add(new ListItem("All", ""));
					this.ForeignKey(fmea);
                    PopulateListControl(SFFilter_DropDownList1, fmea.returnResults, Column.ForeignField);

					string defaultValue = "";
                    FilterLoadEventArgs flea = new FilterLoadEventArgs();
                    flea.FilterDefaults = null;
                    this.OnFilterLoad(flea);

					SFFilter_DropDownList1.SelectedIndex = 0;
                    if (flea.FilterDefaults != null)
                    {
                        flea.FilterDefaults.TryGetValue(Column.FieldName, out defaultValue);
                        if (defaultValue != null) {
                            SFFilter_DropDownList1.SelectedValue = defaultValue;
                        }
                    }
                }
            }
        }
    }
}
