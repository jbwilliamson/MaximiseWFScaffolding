using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class TeacherRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public TeacherRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("TeacherFilterDefault")]string filterData, string sortByExpression)
        {
			string[] includes = new string[] {"Nation","Person"};

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["TeacherSortExpression"] == null ? string.Empty : s["TeacherSortExpression"].ToString();
                    SortDirection sortDirection = s["TeacherSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["TeacherSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(includes, filterData, sortByExpression);
        }

        public int UpdateItem(TKey TeacherId, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(TeacherId, modelMethodContext);
        }

        public int DeleteItem(TKey TeacherId, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(TeacherId, modelMethodContext);
        }
    }
}

