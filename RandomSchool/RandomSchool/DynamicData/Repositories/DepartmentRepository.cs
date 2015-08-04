using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class DepartmentRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public DepartmentRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("DepartmentFilterDefault")]string filterData, string sortByExpression)
        {
			string[] includes = new string[] {"Administrator"};

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["DepartmentSortExpression"] == null ? string.Empty : s["DepartmentSortExpression"].ToString();
                    SortDirection sortDirection = s["DepartmentSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["DepartmentSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(includes, filterData, sortByExpression);
        }

        public int UpdateItem(TKey DepartmentId, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(DepartmentId, modelMethodContext);
        }

        public int DeleteItem(TKey DepartmentId, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(DepartmentId, modelMethodContext);
        }
    }
}

