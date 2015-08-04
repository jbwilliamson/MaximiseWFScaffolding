﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ScaffoldFilter;

namespace RandomSchool.Repositories
{
    public class PersonRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey> where TEntity : class
    {
        public PersonRepository() : base()
        {
        }

		/// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
		/// Can be sorted directed from the listview with optional sortByExpression
        /// Returns an IQueryable list of TEntity which loads directly into the listview
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

        public int UpdateItem(TKey PersonId, ModelMethodContext modelMethodContext)
        {
            return base.UpdateItemBase(PersonId, modelMethodContext);
        }

        public int DeleteItem(TKey PersonId, ModelMethodContext modelMethodContext)
        {
            return base.DeleteItemBase(PersonId, modelMethodContext);
        }
    }
}

