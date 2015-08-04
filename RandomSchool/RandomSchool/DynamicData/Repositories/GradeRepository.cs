using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class GradeRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public GradeRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("GradeFilterDefault")]string filterData, string sortByExpression)
        {
			string[] includes = new string[] {"Course","Grading","Pupil","Year"};

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["GradeSortExpression"] == null ? string.Empty : s["GradeSortExpression"].ToString();
                    SortDirection sortDirection = s["GradeSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["GradeSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(includes, filterData, sortByExpression);
        }

        public int UpdateItem(TKey id, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(id, modelMethodContext);
        }

        public int DeleteItem(TKey id, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(id, modelMethodContext);
        }
    }
}

