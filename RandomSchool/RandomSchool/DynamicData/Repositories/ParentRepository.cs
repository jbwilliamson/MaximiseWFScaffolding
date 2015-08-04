using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class ParentRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public ParentRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("ParentFilterDefault")]string filterData, string sortByExpression)
        {
			

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["ParentSortExpression"] == null ? string.Empty : s["ParentSortExpression"].ToString();
                    SortDirection sortDirection = s["ParentSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["ParentSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(filterData, sortByExpression);
        }

        public int UpdateItem(TKey ParentId, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(ParentId, modelMethodContext);
        }

        public int DeleteItem(TKey ParentId, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(ParentId, modelMethodContext);
        }
    }
}

