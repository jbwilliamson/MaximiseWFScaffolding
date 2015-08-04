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

namespace RandomSchool.Maintain.vPerson
{
    public partial class Default : System.Web.UI.Page
    {
		private PersonRepository<RandomSchool.Models.Person, int> _repository = new PersonRepository<RandomSchool.Models.Person, int>();
		Dictionary<string, string> FilterDefaults = new Dictionary<string, string>();

		protected void Page_Load(object sender, EventArgs e)
        {
            lvPerson.SetDataMethodsObject(_repository);

            if (!IsPostBack)
            {
				DataPager dp = (DataPager)lvPerson.FindControl("dpPerson");
				if (dp != null)
				{
					if (Session["PersonCurrentPage"] != null && Session["PersonPageSize"] != null) {
						dp.SetPageProperties(Convert.ToInt32(Session["PersonCurrentPage"]), Convert.ToInt32(Session["PersonPageSize"]), true);
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
            DataPager dp = (DataPager)lvPerson.FindControl("dpPerson");
            dp.PageSize = Convert.ToInt32(ddl.SelectedValue);
            Session["PersonPageSize"] = dp.PageSize;
            dp.SetPageProperties(0, Convert.ToInt32(Session["PersonPageSize"]), true);
        }

		protected void lvPerson_Sorting(object sender, ListViewSortEventArgs e)
        {
			Session["PersonSortExpression"] = e.SortExpression;
            Session["PersonSortDirection"] = e.SortDirection;
            Session["PersonCurrentPage"] = 0;

			DisplayedSortedArrows();
        }

		protected void lvPerson_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session["PersonCurrentPage"] = e.StartRowIndex;
        }

		protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);

            if (Session["PersonPageSize"] != null) {
                ddl.SelectedValue = Session["PersonPageSize"].ToString();
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

            if (Session["PersonFilterDefault"] == null)
            {
                if (e.SelectedValue.Length > 0)
                {
                    FilterDefaults.Add(e.FieldName, e.SelectedValue);
                    Session["PersonFilterDefault"] = FilterDefaults;
                }
            }
            else
            {
                FilterDefaults = (Dictionary<string, string>)Session["PersonFilterDefault"];

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

                Session["PersonFilterDefault"] = FilterDefaults;
            }

            DataPager dp = (DataPager)lvPerson.FindControl("dpPerson");
            if (dp != null) {
                dp.SetPageProperties(0, dp.PageSize, true);
            }
        }

        protected void ScaffoldFilter_FilterLoad(FilterLoadEventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PersonFilterDefault"] != null) {
                    e.FilterDefaults = (Dictionary<string, string>)Session["PersonFilterDefault"];
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

        protected void lvPerson_LayoutCreated(object sender, EventArgs e)
        {
			DisplayedSortedArrows();
        }

		protected void DisplayedSortedArrows()
        {
            Control headerRow = (Control)lvPerson.FindControl("headerRow");

            if (headerRow != null)
            {
                if (Session["PersonSortExpression"] != null && Session["PersonSortDirection"] != null)
                {
                    string se = Session["PersonSortExpression"].ToString();
                    SortDirection sd = (SortDirection)Session["PersonSortDirection"];

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

