using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RandomSchool.Extenders;
using RandomSchool.Repositories;
using RandomSchool.Filters;
using ScaffoldFilter;

namespace RandomSchool.Maintain.vCourse
{
    public partial class Default : System.Web.UI.Page
    {
		private CourseRepository<RandomSchool.Models.Course, int> _repository = new CourseRepository<RandomSchool.Models.Course, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvCourse.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvCourse.FindControl("dpCourse");
				if (dp != null)
				{
					if (Session["CourseCurrentPage"] != null && Session["CoursePageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["CourseCurrentPage"]), Convert.ToInt32(Session["CoursePageSize"]), true);
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
            DataPager dp = (DataPager)lvCourse.FindControl("dpCourse");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["CoursePageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["CoursePageSize"]), true);
        }

		protected void lvCourse_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["CourseSortExpression"] = e.SortExpression;
            Session["CourseSortDirection"] = e.SortDirection;
            Session["CourseCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvCourse_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["CourseCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["CoursePageSize"] != null) {
                ddl.SelectedValue = Session["CoursePageSize"].ToString();
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

            if (Session["CourseFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["CourseFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["CourseFilterDefault"];

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

                Session["CourseFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvCourse.FindControl("dpCourse");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CourseFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["CourseFilterDefault"];
                }
            }
        }

		public void ForeignKeyEventHandler_LoadForeignKey(ForeignModelEventArgs e)
        {
            e.returnResults = _repository.GetForeignList(e.foreignKeyModel, e.keyType);
        }

        protected void sfForeignKey_Load(object sender, EventArgs e)
        {
            ScaffoldFilterControl scaffoldFilter = (ScaffoldFilterControl)sender;
            ForeignKeyFilter sfuc = scaffoldFilter.FilterTemplate as ForeignKeyFilter;

            if (sfuc != null) {
				sfuc.ForeignKey += new ForeignKeyEventHandler(ForeignKeyEventHandler_LoadForeignKey);
            }
        }

        protected void lvCourse_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvCourse.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["CourseSortExpression"] != null && Session["CourseSortDirection"] != null)
                {
                    string se = Session["CourseSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["CourseSortDirection"];

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

