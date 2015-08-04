using System;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool.Extenders;

namespace RandomSchool.FieldTemplates
{
    public partial class MultilineText_EditField : System.Web.DynamicData.FieldTemplateUserControl 
	{
		public string SetFocus { get; set; }

		protected void Page_Init(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				TextBox1.ToolTip = Column.Description;
				Label1.Text = Column.DisplayName + " :";
            
				if (Column.Prompt != null && Column.Prompt.Length > 0) {
					TextBox1.Attributes.Add("placeholder", Column.Prompt);
				}

				if (SetFocus != null && SetFocus == "True") {
					TextBox1.Attributes.Add("autofocus", "autofocus");
				}

				if (Column.IsRequired)
                {
					TextBox1.Attributes.Add("data-val-required", Column.RequiredErrorMessage == null ? ValidationConstants.Validation_Required_DefaultError : Column.RequiredErrorMessage);
                    TextBox1.Attributes.Add("data-val-clear", "");
                    TextBox1.Attributes.Add("required", "required");

                    TextBox1.Attributes.Add("oninvalid", "setCustomValidity(this.dataset.valRequired);");
					TextBox1.Attributes.Add("oninput", "setCustomValidity(this.validity.valid ? this.dataset.valClear : this.value.length > 0 ? this.dataset.valClear : this.dataset.valRequired);");
				}
			}
        }

		// show bootstrap has-error
		protected void Page_PreRender(object sender, EventArgs e)
        {
            // if validation error then apply bootstrap has-error CSS class
            var isValid = this.Page.ModelState.IsValidField(Column.Name);
            Div1.Attributes["class"] = isValid ? "form-group" : "form-group has-error";
        }

        protected override void ExtractValues(IOrderedDictionary dictionary) {
            dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
        }
    
        public override Control DataControl {
            get {
                return TextBox1;
            }
        }
    }
}
