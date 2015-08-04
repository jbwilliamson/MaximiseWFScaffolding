using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using RandomSchoolAsync.Extenders;
using System.Threading.Tasks;
using ScaffoldFilter;

namespace RandomSchoolAsync.FieldTemplates
{
    public partial class ForeignKey_EditField : System.Web.DynamicData.FieldTemplateUserControl 
	{
        public string DataTypeName { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }

		public string SetFocus { get; set; }
		private string SetValue;
        private string sortByColumn = "";
        private bool sortDescending = false;

		public event AsyncForeignKeyEventHandler ForeignKey; 

        protected void Page_Init(object sender, EventArgs e) 
		{
			if (!IsPostBack)
			{
				Label1.Text = Column.DisplayName + " :";

				if (SetFocus != null && SetFocus == "True") {
					DropDownList1.Attributes.Add("autofocus", "autofocus");
				}

				if (Column.IsRequired)
                {
					DropDownList1.Attributes.Add("data-val-required", Column.RequiredErrorMessage == null ? ValidationConstants.Validation_Required_DefaultError : Column.RequiredErrorMessage);
                    DropDownList1.Attributes.Add("data-val-clear", "");
                    DropDownList1.Attributes.Add("required", "required");

					DropDownList1.Attributes.Add("oninvalid", "setCustomValidity(this.dataset.valRequired);");
                    DropDownList1.Attributes.Add("onchange", "setCustomValidity(this.dataset.valClear);");
				}

                UIHintAttribute uihintAttr = Column.Attributes.OfType<UIHintAttribute>().FirstOrDefault();

                if (uihintAttr != null)
                {
                    object cpSortBy = null;
                    object cpDescendingOrder = null;

                    if (uihintAttr.ControlParameters.ContainsKey(ScaffoldConst.ForeignKey_RequireSorting)) {
                        cpSortBy = uihintAttr.ControlParameters[ScaffoldConst.ForeignKey_RequireSorting];
                    }

                    if (uihintAttr.ControlParameters.ContainsKey(ScaffoldConst.ForeignKey_SortDesending)) {
                        cpDescendingOrder = uihintAttr.ControlParameters[ScaffoldConst.ForeignKey_SortDesending];
                    }

                    sortByColumn = string.Empty;
                    if (cpSortBy != null) {
                        sortByColumn = cpSortBy.ToString();
                    }

                    sortDescending = false;
                    if (cpDescendingOrder != null) {
                        sortDescending = Convert.ToBoolean(cpDescendingOrder);
                    }   
                }
			}
        }
		
		// show bootstrap has-error
		protected void Page_PreRender(object sender, EventArgs e)
        {
			string defaultValue = "";
            // if validation error then apply bootstrap has-error CSS class
            var isValid = this.Page.ModelState.IsValidField(Column.Name);
            Div1.Attributes["class"] = isValid ? "form-group" : "form-group has-error";

			if (!IsPostBack)
			{
				if (this.Mode == DataBoundControlMode.Insert)
				{
					DropDownList1.Items.Insert(0, new ListItem("Select An Option", ""));
					DropDownList1.SelectedIndex = 0;

					defaultValue = Convert.ToString(Column.DefaultValue);

                    if (defaultValue != string.Empty)
                    {
                        ListItem li = DropDownList1.Items.FindByValue(defaultValue);
                        if (li != null) {
                            DropDownList1.SelectedValue = defaultValue;
                        }
                    }
				}

                if (this.Mode == DataBoundControlMode.Edit)
                {
                    if (DropDownList1.Items.Count > 0 && SetValue != null)
                    {
                        if (SetValue == string.Empty)
                        {
                            if (DropDownList1.Items[0].Text != "Not Set") {
                                DropDownList1.Items.Insert(0, new ListItem("Not Set", ""));
                            }
                            DropDownList1.SelectedIndex = 0;
                        }
                        else {
                            DropDownList1.SelectedValue = SetValue;
                        }
                    }
                    else
                    {
                        if (DropDownList1.Items.Count == 0) {
                            DropDownList1.Items.Insert(0, new ListItem("None Available", ""));
                        }
                    }
                }
			}
        }

		protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            
			if (!IsPostBack)
			{
				if (this.Mode == DataBoundControlMode.Edit) {
                    SetValue = FieldValueString;
				}
			}
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // If it's an empty string, change it to null
            string value = DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(value)) {
                value = null;
            }

            dictionary[Column.Name] = ConvertEditedValue(value);
        }

        public override Control DataControl {
            get {
                return DropDownList1;
            }
        }

		protected void DropDownList1_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ForeignModelEventArgs fmea = new ForeignModelEventArgs();
                fmea.returnResults = null;
                fmea.foreignKeyModel = DataTypeName;
				fmea.keyType = ForeignModelEventArgs.LoadForeignTableByModel;

				this.Page.RegisterAsyncTask(new PageAsyncTask(async () =>
				{
					if (this.ForeignKey != null) {
						await this.ForeignKey(fmea);
						PopulateListControl(fmea.returnResults, DataValueField, DataTextField);
					}
				}));
            }
        }

        protected void PopulateListControl(IQueryable dataSource, string valueField, string keyField)
        {
            var selectData = new List<KeyValuePair<string, string>>();

            if (dataSource != null)
            {
                foreach (object dataItem in dataSource) {
                    selectData.Add(new KeyValuePair<string, string>(DataBinder.GetPropertyValue(dataItem, keyField, null), DataBinder.GetPropertyValue(dataItem, valueField, null)));
                }

                if (sortByColumn == "Yes")
                {
                    if (sortDescending)
                        selectData.Sort(CompareDesc);
                    else
                        selectData.Sort(CompareAsc);
                }

				DropDownList1.AppendDataBoundItems = true;
                DropDownList1.DataSource = selectData;
                DropDownList1.DataTextField = "Key";
                DropDownList1.DataValueField = "Value";
                DropDownList1.DataBind();
                
				if (SetValue != null)
                {
                    if (SetValue == string.Empty)
                    {
                        DropDownList1.Items.Insert(0, new ListItem("Not Set", ""));
                        DropDownList1.SelectedIndex = 0;
                    }
                    else {
                        DropDownList1.SelectedValue = SetValue;
                    }
                }
            }
        }

        static int CompareAsc(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }
        
        static int CompareDesc(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return b.Key.CompareTo(a.Key);
        }
    }
}
