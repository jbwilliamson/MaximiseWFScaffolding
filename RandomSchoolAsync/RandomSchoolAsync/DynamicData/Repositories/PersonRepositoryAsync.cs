using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using ScaffoldFilter;

namespace RandomSchoolAsync.Repositories
{
	public class PersonRepositoryAsync<TEntity, TKey> : RepositoryAsyncBase<TEntity, TKey> where TEntity : class
    {
        public PersonRepositoryAsync() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
        /// Returns an IQuerybale list of TEntity which loads directly into the listview
        /// 'ScaffoldFilterParameter' is a custom class to extract the filter data
        /// </summary>
        public IQueryable<TEntity> GetData([ScaffoldFilterParameter("PersonFilterDefault")]string filterData, string sortByExpression)
		{
			string[] includes = new string[] {"School"};

            if (sortByExpression == null)
            {
                System.Web.SessionState.HttpSessionState s = System.Web.HttpContext.Current.Session;

                if (s != null)
                {
                    string sortExpression = s["PersonSortExpression"] == null ? string.Empty : s["PersonSortExpression"].ToString();
                    SortDirection sortDirection = s["PersonSortDirection"] == null ? SortDirection.Ascending : (SortDirection)s["PersonSortDirection"];

                    if (sortExpression != string.Empty) {
                        sortByExpression = sortExpression + (sortDirection == SortDirection.Ascending ? "" : " DESC");
                    }
                }
            }

			return base.GetModelData(includes, filterData, sortByExpression);
        }

        public async Task<int> InsertItem(ModelMethodContext modelMethodContext)
        {
            return await base.InsertItemAsync(modelMethodContext);
        }

        public async Task<int> UpdateItem(TKey PersonId, ModelMethodContext modelMethodContext)
        {
            return await base.UpdateItemAsync(PersonId, modelMethodContext);
        }

        public async Task<int> DeleteItem(TKey PersonId, ModelMethodContext modelMethodContext)
        {
            return await base.DeleteItemAsync(PersonId, modelMethodContext);
        }
    }
}

