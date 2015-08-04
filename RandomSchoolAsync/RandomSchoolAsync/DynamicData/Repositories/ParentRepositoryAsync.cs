using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using ScaffoldFilter;

namespace RandomSchoolAsync.Repositories
{
	public class ParentRepositoryAsync<TEntity, TKey> : RepositoryAsyncBase<TEntity, TKey> where TEntity : class
    {
        public ParentRepositoryAsync() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
        /// Returns an IQuerybale list of TEntity which loads directly into the listview
        /// 'ScaffoldFilterParameter' is a custom class to extract the filter data
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

        public async Task<int> InsertItem(ModelMethodContext modelMethodContext)
        {
            return await base.InsertItemAsync(modelMethodContext);
        }

        public async Task<int> UpdateItem(TKey ParentId, ModelMethodContext modelMethodContext)
        {
            return await base.UpdateItemAsync(ParentId, modelMethodContext);
        }

        public async Task<int> DeleteItem(TKey ParentId, ModelMethodContext modelMethodContext)
        {
            return await base.DeleteItemAsync(ParentId, modelMethodContext);
        }
    }
}

