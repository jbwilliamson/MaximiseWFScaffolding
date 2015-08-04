using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class CourseRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public CourseRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("CourseFilterDefault")]string filterData, string sortByExpression)
        {
			string[] includes = new string[] {"Department","Room"};

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["CourseSortExpression"] == null ? string.Empty : s["CourseSortExpression"].ToString();
                    SortDirection sortDirection = s["CourseSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["CourseSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(includes, filterData, sortByExpression);
        }

        public int UpdateItem(TKey CourseId, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(CourseId, modelMethodContext);
        }

        public int DeleteItem(TKey CourseId, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(CourseId, modelMethodContext);
        }
    }
}

