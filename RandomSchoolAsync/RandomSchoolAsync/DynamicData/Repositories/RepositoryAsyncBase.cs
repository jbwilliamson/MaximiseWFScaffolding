using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using System.Reflection;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading.Tasks;

using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using ScaffoldFilter;
using RandomSchoolAsync.Extenders;

namespace RandomSchoolAsync.Repositories
{
	public class RepositoryAsyncBase<TEntity, TKey> : IRepositoryAsync<TEntity, TKey> where TEntity : class
    {
		private readonly RandomSchoolAsync.DAL.SchoolContext dbrepContext = new RandomSchoolAsync.DAL.SchoolContext();
        private readonly DbSet<TEntity> dbrepSet;

        public RepositoryAsyncBase()
        {
            dbrepSet = dbrepContext.Set<TEntity>();
        }

		/// <summary>
        /// Retrieves one item of the TEntity class for edit or display
		/// Note: Async and model binding do not play nice together, FormView will not bind to a return of a Task, so no async for getitem
        /// </summary>
        public TEntity GetItem([FriendlyUrlSegmentsAttribute(0)] TKey id)
        {
			if (id == null) {
                return null;
            }

            return this.dbrepSet.Find(id);
        }

        /// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// May include any foreign key tables for TEntity model
        /// Returns an IQuerybale list of TEntity which loads directly into the listview
		/// Note: Async and model binding do not play nice together, listview will not bind to a return of the Task proxy class, so no async for getdata
        /// </summary>
		public IQueryable<TEntity> GetModelData(string[] includeModel, string filterData, string sortByExpression)
        {
			IQueryable<TEntity> query = dbrepSet.AsQueryable();

			if (!string.IsNullOrEmpty(filterData)) {
				query = dbrepSet.Where(FilterOnModel(filterData)).AsQueryable<TEntity>();
			}
			
			foreach (string m in includeModel) {
                if (m.Length > 0) {
                    query = query.Include(m);
                }
            }

            if (sortByExpression != null)
            {
                if (sortByExpression.EndsWith(" DESC")) {
                    query = query.OrderByDescending(sortByExpression.Substring(0, sortByExpression.Length - 5));
                }
                else {
                    query = query.OrderBy(sortByExpression);
                }
            }

            return query;
        }

        /// <summary>
        /// Load the default list of items from the database, which may include one or more filter options
        /// Returns an IQuerybale list of TEntity which loads directly into the listview
        /// </summary>
		public IQueryable<TEntity> GetModelData(string filterData, string sortByExpression)
        {
			IQueryable<TEntity> query = dbrepSet.AsQueryable();

			if (!string.IsNullOrEmpty(filterData)) {
				query = dbrepSet.Where(FilterOnModel(filterData)).AsQueryable<TEntity>();
			}

            if (sortByExpression != null)
            {
                if (sortByExpression.EndsWith(" DESC")) {
                    query = query.OrderByDescending(sortByExpression.Substring(0, sortByExpression.Length - 5));
                }
                else {
                    query = query.OrderBy(sortByExpression);
                }
            }

			return query;
        }

		/// <summary>
        /// Function to generate and compile a linq expression, based on the filter parameters passed to 'GetData'
        /// </summary>
        private Func<TEntity, bool> FilterOnModel(string filterData)
        {
            string[] filterParameters = filterData.Split(',');
            var type = typeof(TEntity);
            var pe = Expression.Parameter(type, "p");
            Expression filterExpression = null;

            foreach (string parameter in filterParameters)
            {
                string[] filterValues = parameter.Split(':');

                PropertyInfo modelField = type.GetProperties().Where(p => p.Name == filterValues[0]).FirstOrDefault();
                if (modelField != null)
                {
                    var propertyReference = Expression.Property(pe, filterValues[0]);
 
                    object value = TypeDescriptor.GetConverter(modelField.PropertyType).ConvertFromString(filterValues[1]);
                    var propertyValue = Expression.Constant(value, modelField.PropertyType);
                    var compareExp = Expression.Equal(propertyReference, propertyValue);

                    filterExpression = filterExpression == null ? compareExp  : Expression.AndAlso(filterExpression, compareExp);
                }
            }

            return Expression.Lambda<Func<TEntity, bool>>(filterExpression, pe).Compile();
        }

		/// <summary>
        /// Generic function to retrieve a foreignkey table, returns an IQueryable of unknow type
        /// keyType = ForeignModelEventArgs.LoadForeignTableByKey (foreignKeyModel parameter contains the foreignkey field name, if so then we need to find the model)
        /// keyType = ForeignModelEventArgs.LoadForeignTableByModel (foreignKeyModel parmeter contains the foreignkey model name)
        /// </summary>
		public async Task<IQueryable> GetForeignListAsync(string foreignKeyModel, int keyType)
        {
            string includeModel = foreignKeyModel;

            if (keyType == ForeignModelEventArgs.LoadForeignTableByKey && !string.IsNullOrEmpty(foreignKeyModel)) {
                includeModel = FindForeignKeyRelationship(foreignKeyModel);
            }

            if (!string.IsNullOrEmpty(includeModel))
            {
                Type inModel = Type.GetType(includeModel);

				if (inModel != null)
                {
                    DbSet fset = dbrepContext.Set(inModel);
                    await fset.LoadAsync();
                    return fset.Local.AsQueryable();
				}
            }

            return null;
        }

		/// <summary>
        /// Looks through the entity framework to retreive the model name for the foreign key table based on a foreign key field name
        /// Normally only used on filter fields
        /// </summary>
        private string FindForeignKeyRelationship(string foreignKey)
        {
            // Load EF metadata
            var metadata = ((IObjectContextAdapter)dbrepContext).ObjectContext.MetadataWorkspace;
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Find only objects from the current model <TEntity>
            var entityType = metadata
                .GetItems<EntityType>(DataSpace.OSpace)
                .Single(e => objectItemCollection.GetClrType(e) == typeof(TEntity));

            // Find the foreignkey constaint which matches the filter field 
            var association = metadata
                .GetItems<AssociationType>(DataSpace.SSpace)
                .Where(a => a.ReferentialConstraints.Any(o => o.ToProperties.Any(x => x.Name == foreignKey)));

            // Match the filter field foreign key to the database model
            var navProp = entityType.Members.OfType<NavigationProperty>().Where(a => association.Any(o => o.Name == a.RelationshipType.Name)).SingleOrDefault();

            if (navProp != null) {
                // Return the name of the model
                return navProp.TypeUsage.EdmType.FullName;
            }

            return "";
        }

        /// <summary>
        /// Async inserts a record into the database using model binding
        /// </summary>
        public async Task<int> InsertItemAsync(ModelMethodContext modelMethodContext)
        {
			int ret = 0;
            var item = this.dbrepSet.Create();
            modelMethodContext.TryUpdateModel(item);

            if (modelMethodContext.ModelState.IsValid)
            {
                this.dbrepSet.Add(item);

				try
                {
                    ret = await dbrepContext.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (DbUpdateException ex)
                {
                    modelMethodContext.ModelState.AddModelError("InsertError", ex.InnerException.InnerException.Message);
                    ret = 0;
                }

                return ret;
            }

            return 0;
        }

        /// <summary>
        /// Async updates a record in the database using model binding
        /// </summary>
        public async Task<int> UpdateItemAsync(TKey id, ModelMethodContext modelMethodContext)
        {
			int ret;
            var item = this.dbrepSet.Find(id);
            if (item == null)
            {
                modelMethodContext.ModelState.AddModelError("idNotFound", String.Format("Item with id {0} was not found", id == null ? "null" : id.ToString()));
                return 0;
            }

            modelMethodContext.TryUpdateModel(item);
            if (modelMethodContext.ModelState.IsValid)
            {
                try
                {
                    ret = await dbrepContext.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (DbUpdateException ex)
                {
                    modelMethodContext.ModelState.AddModelError("UpdateError", ex.InnerException.InnerException.Message);
                    ret = 0;
                }

                return ret;
            }
            
            return 0;
        }

        /// <summary>
        /// Async deletes a record from the database
        /// </summary>
        public async Task<int> DeleteItemAsync(TKey id, ModelMethodContext modelMethodContext)
        {
            var item = this.dbrepSet.Find(id);
            if (item != null)
            {
                this.dbrepSet.Remove(item);
                return await dbrepContext.SaveChangesAsync().ConfigureAwait(false);
            }
			else
			{
				modelMethodContext.ModelState.AddModelError("idNotFound", String.Format("A Item with id {0} was not found", id == null ? "null" : id.ToString()));
			}

            return 0;
        }
    }
}