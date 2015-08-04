﻿using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandomSchoolAsync.FieldTemplates 
{
    public partial class Boolean_EditField : System.Web.DynamicData.FieldTemplateUserControl {

		public string SetFocus { get; set; }

		protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
			{
				CheckBox1.ToolTip = Column.Description;
				Label1.Text = Column.DisplayName + " :";

				if (SetFocus != null && SetFocus == "True") {
					CheckBox1.Attributes.Add("autofocus", "autofocus");
				}
			}
        }

        protected override void OnDataBinding(EventArgs e) {
            base.OnDataBinding(e);
    
            object val = FieldValue;
            if (val != null)
                CheckBox1.Checked = (bool) val;
        }
    
        protected override void ExtractValues(IOrderedDictionary dictionary) {
            dictionary[Column.Name] = CheckBox1.Checked;
        }
    
        public override Control DataControl {
            get {
                return CheckBox1;
            }
        }
    }
}
