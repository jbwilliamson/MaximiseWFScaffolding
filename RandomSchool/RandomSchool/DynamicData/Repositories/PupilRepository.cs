using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class PupilRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public PupilRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("PupilFilterDefault")]string filterData, string sortByExpression)
        {
			string[] includes = new string[] {"FirstParent","Nation","Person","SecondParent"};

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["PupilSortExpression"] == null ? string.Empty : s["PupilSortExpression"].ToString();
                    SortDirection sortDirection = s["PupilSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["PupilSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(includes, filterData, sortByExpression);
        }

        public int UpdateItem(TKey StudentId, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(StudentId, modelMethodContext);
        }

        public int DeleteItem(TKey StudentId, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(StudentId, modelMethodContext);
        }
    }
}

