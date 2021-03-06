﻿using System;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RandomSchool .Extenders;

namespace RandomSchool.FieldTemplates
{
    public partial class DateTime_EditField : System.Web.DynamicData.FieldTemplateUserControl 
	{
        private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);
  
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

				if (SetFocus != null && SetFocus == "True")	{
					TextBox1.Attributes.Add("autofocus", "autofocus");
				}

				RangeAttribute rangeAttr = Column.Attributes.OfType<RangeAttribute>().FirstOrDefault();
				
				TextBox1.Attributes.Add("data-val-clear", "");
                if (Column.IsRequired) {
                    TextBox1.Attributes.Add("oninput", "setCustomValidity(this.validity.valid ? this.dataset.valClear : this.value.length > 0 ? this.dataset.valClear  : this.dataset.valRequired);");
                }
                else {
				    TextBox1.Attributes.Add("oninput", "setCustomValidity(this.validity.valid ? this.dataset.valClear : this.validity.patternMismatch ? this.dataset.valRegex : (this.validity.rangeUnderflow || this.validity.rangeOverflow) ? this.dataset.valRange : this.dataset.valClear);");
                }

				if (Column.IsRequired)
                {
                    TextBox1.Attributes.Add("data-val-required", Column.RequiredErrorMessage == null ? ValidationConstants.Validation_Required_DefaultError : Column.RequiredErrorMessage);
                    TextBox1.Attributes.Add("required", "required");
                    errorMessage = "this.validity.valueMissing ? this.dataset.valRequired : ";
				}

                if (rangeAttr != null)
                {
                    if (rangeAttr.Minimum != null) {
                        TextBox1.Attributes.Add("min", rangeAttr.Minimum.ToString());
                    }

                    if (rangeAttr.Maximum != null) {
                        TextBox1.Attributes.Add("max", rangeAttr.Maximum.ToString());
                    }

                    TextBox1.Attributes.Add("data-val-range", rangeAttr.ErrorMessage == null ? ValidationConstants.Validation_Range_DefaultError : rangeAttr.ErrorMessage);
                    errorMessage = errorMessage + "(this.validity.rangeUnderflow || this.validity.rangeOverflow) ? this.dataset.valRange : ";
                }

				// Regular expression to validate a date will not work for model binding (regex would be performed on the datetime type and not a string), so regexp is added here and not in the dataannotations
				TextBox1.Attributes.Add("pattern", ValidationConstants.DateTime_ValidationRegExpr);
				TextBox1.Attributes.Add("data-val-regex", ValidationConstants.DateTime_ValidationRegError);
				errorMessage = errorMessage + "this.validity.patternMismatch ? this.dataset.valRegex : ";

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
