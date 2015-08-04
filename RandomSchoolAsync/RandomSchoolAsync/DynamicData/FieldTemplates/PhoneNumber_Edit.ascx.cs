using System;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchoolAsync.Extenders;

namespace RandomSchoolAsync.FieldTemplates
{
    public partial class PhoneNumber_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
		public string SetFocus { get; set; }

		protected void Page_Init(object sender, EventArgs e) 
		{
            if (!IsPostBack)
			{
				string errorMessage = "";
				TextBox1.ToolTip = Column.Description;
				Label1.Text = Column.DisplayName + " :";
			
				if (Column.Prompt != null && Column.Prompt.Length > 0) {
					TextBox1.Attributes.Add("placeholder", Column.Prompt);
				}

				if (SetFocus != null && SetFocus == "True") {
					TextBox1.Attributes.Add("autofocus", "autofocus");
				}

				RegularExpressionAttribute regularExpressionAttr = Column.Attributes.OfType<RegularExpressionAttribute>().SingleOrDefault();
                
				if (Column.IsRequired || regularExpressionAttr != null)
                {
                    TextBox1.Attributes.Add("data-val-clear", "");
                    if (Column.IsRequired) {
                        TextBox1.Attributes.Add("oninput", "setCustomValidity(this.validity.valid ? this.dataset.valClear : this.value.length > 0 ? this.dataset.valClear : this.dataset.valRequired);");
                    }
                    else {
                        TextBox1.Attributes.Add("oninput", "setCustomValidity(this.validity.valid ? this.dataset.valClear : this.title);");
                    }
                }

				if (Column.IsRequired)
                {
					TextBox1.Attributes.Add("data-val-required", Column.RequiredErrorMessage == null ? ValidationConstants.Validation_Required_DefaultError : Column.RequiredErrorMessage);
                    TextBox1.Attributes.Add("required", "required");
					errorMessage = "this.validity.valueMissing ? this.dataset.valRequired : ";
				}

				if (regularExpressionAttr != null && regularExpressionAttr.Pattern != null && regularExpressionAttr.Pattern.Length > 0)
				{
					TextBox1.Attributes.Add("pattern", regularExpressionAttr.Pattern);
					TextBox1.Attributes.Add("data-val-regex", regularExpressionAttr.ErrorMessage == null ? ValidationConstants.Validation_Pattern_DefaultError : regularExpressionAttr.ErrorMessage);
					errorMessage = errorMessage + "this.validity.patternMismatch ? this.dataset.valRegex : ";
				}
                
				errorMessage = "setCustomValidity(" + errorMessage + "this.dataset.valClear);";
                TextBox1.Attributes.Add("oninvalid", errorMessage);
			}
		}

		// show bootstrap has-error
		protected void Page_PreRender(object sender, EventArgs e)
        {
            // if validation error then apply bootstrap has-error CSS class
            var isValid = this.Page.ModelState.IsValidField(Column.Name);
            Div1.Attributes["class"] = isValid ? "form-group" : "form-group has-error";
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (Column.MaxLength > 0)
            {
                TextBox1.MaxLength = Math.Max(FieldValueEditString.Length, Column.MaxLength);
            }
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
        }

        public override Control DataControl
        {
            get
            {
                return TextBox1;
            }
        }
    }
}
