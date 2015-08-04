using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandomSchool {
    public partial class LabelControl : System.Web.UI.UserControl {
        private const int MAX_DISPLAYLENGTH_IN_LIST = 25;
    
		public string ForeignKeyText { get; set; }

		protected void Page_Load(object sender, EventArgs e) {
        }

        public string LabelTextValue() {
            string value = Server.HtmlDecode(this.ForeignKeyText ?? "");

            if (value.Length > MAX_DISPLAYLENGTH_IN_LIST) {
                value = value.Substring(0, MAX_DISPLAYLENGTH_IN_LIST - 3) + "...";
            }

            return value;
		}

        public string ToolTipText() {
            return Server.HtmlDecode(this.ForeignKeyText ?? "");
        }
    }
}
