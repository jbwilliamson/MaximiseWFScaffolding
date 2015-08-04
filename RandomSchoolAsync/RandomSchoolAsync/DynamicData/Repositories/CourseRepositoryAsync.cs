﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using ScaffoldFilter;

namespace RandomSchoolAsync.Repositories
{
	public class CourseRepositoryAsync<TEntity, TKey> : RepositoryAsyncBase<TEntity, TKey> where TEntity : class
    {
        public CourseRepositoryAsync() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
        /// Returns an IQuerybale list of TEntity which loads directly into the listview
        /// 'ScaffoldFilterParameter' is a custom class to extract the filter data
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

        public async Task<int> InsertItem(ModelMethodContext modelMethodContext)
        {
            return await base.InsertItemAsync(modelMethodContext);
        }

        public async Task<int> UpdateItem(TKey CourseId, ModelMethodContext modelMethodContext)
        {
            return await base.UpdateItemAsync(CourseId, modelMethodContext);
        }

        public async Task<int> DeleteItem(TKey CourseId, ModelMethodContext modelMethodContext)
        {
            return await base.DeleteItemAsync(CourseId, modelMethodContext);
        }
    }
}

