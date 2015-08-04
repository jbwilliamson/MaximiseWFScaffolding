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

namespace RandomSchoolAsync.Maintain.vTeacher
{
    public partial class Default : System.Web.UI.Page
    {
		private TeacherRepositoryAsync<RandomSchoolAsync.Models.Teacher, int> _repository = new TeacherRepositoryAsync<RandomSchoolAsync.Models.Teacher, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvTeacher.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvTeacher.FindControl("dpTeacher");
				if (dp != null)
				{
					if (Session["TeacherCurrentPage"] != null && Session["TeacherPageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["TeacherCurrentPage"]), Convert.ToInt32(Session["TeacherPageSize"]), true);
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
            DataPager dp = (DataPager)lvTeacher.FindControl("dpTeacher");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["TeacherPageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["TeacherPageSize"]), true);
        }

		protected void lvTeacher_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["TeacherSortExpression"] = e.SortExpression;
            Session["TeacherSortDirection"] = e.SortDirection;
            Session["TeacherCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvTeacher_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["TeacherCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["TeacherPageSize"] != null) {
                ddl.SelectedValue = Session["TeacherPageSize"].ToString();
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

            if (Session["TeacherFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["TeacherFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["TeacherFilterDefault"];

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

                Session["TeacherFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvTeacher.FindControl("dpTeacher");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TeacherFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["TeacherFilterDefault"];
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

        protected void lvTeacher_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvTeacher.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["TeacherSortExpression"] != null && Session["TeacherSortDirection"] != null)
                {
                    string se = Session["TeacherSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["TeacherSortDirection"];

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

