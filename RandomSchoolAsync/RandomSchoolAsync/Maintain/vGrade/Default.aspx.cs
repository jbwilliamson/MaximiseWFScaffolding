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

namespace RandomSchoolAsync.Maintain.vGrade
{
    public partial class Default : System.Web.UI.Page
    {
		private GradeRepositoryAsync<RandomSchoolAsync.Models.Grade, int> _repository = new GradeRepositoryAsync<RandomSchoolAsync.Models.Grade, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvGrade.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvGrade.FindControl("dpGrade");
				if (dp != null)
				{
					if (Session["GradeCurrentPage"] != null && Session["GradePageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["GradeCurrentPage"]), Convert.ToInt32(Session["GradePageSize"]), true);
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
            DataPager dp = (DataPager)lvGrade.FindControl("dpGrade");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["GradePageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["GradePageSize"]), true);
        }

		protected void lvGrade_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["GradeSortExpression"] = e.SortExpression;
            Session["GradeSortDirection"] = e.SortDirection;
            Session["GradeCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvGrade_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["GradeCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["GradePageSize"] != null) {
                ddl.SelectedValue = Session["GradePageSize"].ToString();
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

            if (Session["GradeFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["GradeFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["GradeFilterDefault"];

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

                Session["GradeFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvGrade.FindControl("dpGrade");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["GradeFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["GradeFilterDefault"];
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

        protected void lvGrade_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvGrade.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["GradeSortExpression"] != null && Session["GradeSortDirection"] != null)
                {
                    string se = Session["GradeSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["GradeSortDirection"];

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

