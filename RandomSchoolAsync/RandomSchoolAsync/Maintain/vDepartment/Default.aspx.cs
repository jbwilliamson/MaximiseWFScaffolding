using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RandomSchoolAsync.Extenders;
using RandomSchoolAsync.Repositories;
using RandomSchoolAsync.Filters;
using System.Threading.Tasks;
using ScaffoldFilter;

namespace RandomSchoolAsync.Maintain.vDepartment
{
    public partial class Default : System.Web.UI.Page
    {
		private DepartmentRepositoryAsync<RandomSchoolAsync.Models.Department, int> _repository = new DepartmentRepositoryAsync<RandomSchoolAsync.Models.Department, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvDepartment.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvDepartment.FindControl("dpDepartment");
				if (dp != null)
				{
					if (Session["DepartmentCurrentPage"] != null && Session["DepartmentPageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["DepartmentCurrentPage"]), Convert.ToInt32(Session["DepartmentPageSize"]), true);
					}
					else {
						dp.SetPageProperties(0, 10, true);
					}
				}
            }
        }

		protected void ddlPageSize_SelectedIndexChanged(object sender,  EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);
            DataPager dp = (DataPager)lvDepartment.FindControl("dpDepartment");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["DepartmentPageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["DepartmentPageSize"]), true);
        }

		protected void lvDepartment_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["DepartmentSortExpression"] = e.SortExpression;
            Session["DepartmentSortDirection"] = e.SortDirection;
            Session["DepartmentCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvDepartment_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["DepartmentCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["DepartmentPageSize"] != null) {
                ddl.SelectedValue = Session["DepartmentPageSize"].ToString();
            }
        }

		protected void ScaffoldLabel_PreRender(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            ScaffoldFilterControl scaffoldFilter = (ScaffoldFilterControl)label.FindControl("ScaffoldFilter");
            ScaffoldFilterUserControl fuc = scaffoldFilter.FilterTemplate as ScaffoldFilterUserControl;
            if (fuc != null && fuc.FilterControl != null) {
                label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
            }
        }

        protected void ScaffoldFilter_FilterChanged(object sender, FilterChangeEventArgs e)
        {
            string val;

            if (Session["DepartmentFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["DepartmentFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["DepartmentFilterDefault"];

                if (FilterDefaults.TryGetValue(e.FieldName, out val)) 
                {
                    if (e.SelectedValue.Length > 0) {
                        FilterDefaults[e.FieldName] = e.SelectedValue;
                    }
                    else {
                        FilterDefaults.Remove(e.FieldName);
                    }
                }
                else 
                {
                    if (e.SelectedValue.Length > 0) {
                        FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    }
                }

                Session["DepartmentFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvDepartment.FindControl("dpDepartment");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["DepartmentFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["DepartmentFilterDefault"];
                }
            }
        }

		public async Task ForeignKeyEventHandler_LoadForeignKey(ForeignModelEventArgs e)
        {
            e.returnResults = await _repository.GetForeignListAsync(e.foreignKeyModel, e.keyType);
        }

        protected void sfForeignKey_Load(object sender, EventArgs e)
        {
            ScaffoldFilterControl scaffoldFilter = (ScaffoldFilterControl)sender;
            ForeignKeyFilter sfuc = scaffoldFilter.FilterTemplate as ForeignKeyFilter;

            if (sfuc != null) {
                sfuc.ForeignKey += new AsyncForeignKeyEventHandler(ForeignKeyEventHandler_LoadForeignKey);
            }
        }

        protected void lvDepartment_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvDepartment.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["DepartmentSortExpression"] != null && Session["DepartmentSortDirection"] != null)
                {
                    string se = Session["DepartmentSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["DepartmentSortDirection"];

                    foreach (HtmlControl tableCell in headerRow.Controls)
                    {
                        if (tableCell.GetType() == typeof(HtmlTableCell))
                        {
                            IButtonControl btnSortField = tableCell.Controls.OfType<IButtonControl>().SingleOrDefault();
                            HtmlGenericControl gcArrow = tableCell.Controls.OfType<HtmlGenericControl>().SingleOrDefault();

                            if (btnSortField != null && gcArrow != null)
                            {
                                if (btnSortField.CommandArgument == se)
                                    gcArrow.Attributes["class"] = sd == SortDirection.Ascending ? "glyphicon glyphicon-chevron-up" : "glyphicon glyphicon-chevron-down";
                                else
                                {
                                    if (gcArrow.Attributes["class"] != null) gcArrow.Attributes.Remove("class");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

