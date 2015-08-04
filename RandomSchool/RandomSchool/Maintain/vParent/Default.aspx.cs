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

namespace RandomSchool.Maintain.vParent
{
    public partial class Default : System.Web.UI.Page
    {
		private ParentRepository<RandomSchool.Models.Parent, int> _repository = new ParentRepository<RandomSchool.Models.Parent, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvParent.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvParent.FindControl("dpParent");
				if (dp != null)
				{
					if (Session["ParentCurrentPage"] != null && Session["ParentPageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["ParentCurrentPage"]), Convert.ToInt32(Session["ParentPageSize"]), true);
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
            DataPager dp = (DataPager)lvParent.FindControl("dpParent");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["ParentPageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["ParentPageSize"]), true);
        }

		protected void lvParent_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["ParentSortExpression"] = e.SortExpression;
            Session["ParentSortDirection"] = e.SortDirection;
            Session["ParentCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvParent_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["ParentCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["ParentPageSize"] != null) {
                ddl.SelectedValue = Session["ParentPageSize"].ToString();
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

            if (Session["ParentFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["ParentFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["ParentFilterDefault"];

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

                Session["ParentFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvParent.FindControl("dpParent");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ParentFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["ParentFilterDefault"];
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

        protected void lvParent_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvParent.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["ParentSortExpression"] != null && Session["ParentSortDirection"] != null)
                {
                    string se = Session["ParentSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["ParentSortDirection"];

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

