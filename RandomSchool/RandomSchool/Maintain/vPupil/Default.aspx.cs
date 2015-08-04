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

namespace RandomSchool.Maintain.vPupil
{
    public partial class Default : System.Web.UI.Page
    {
		private PupilRepository<RandomSchool.Models.Pupil, int> _repository = new PupilRepository<RandomSchool.Models.Pupil, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvPupil.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvPupil.FindControl("dpPupil");
				if (dp != null)
				{
					if (Session["PupilCurrentPage"] != null && Session["PupilPageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["PupilCurrentPage"]), Convert.ToInt32(Session["PupilPageSize"]), true);
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
            DataPager dp = (DataPager)lvPupil.FindControl("dpPupil");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["PupilPageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["PupilPageSize"]), true);
        }

		protected void lvPupil_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["PupilSortExpression"] = e.SortExpression;
            Session["PupilSortDirection"] = e.SortDirection;
            Session["PupilCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvPupil_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["PupilCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["PupilPageSize"] != null) {
                ddl.SelectedValue = Session["PupilPageSize"].ToString();
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

            if (Session["PupilFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["PupilFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["PupilFilterDefault"];

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

                Session["PupilFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvPupil.FindControl("dpPupil");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PupilFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["PupilFilterDefault"];
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

        protected void lvPupil_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvPupil.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["PupilSortExpression"] != null && Session["PupilSortDirection"] != null)
                {
                    string se = Session["PupilSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["PupilSortDirection"];

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

