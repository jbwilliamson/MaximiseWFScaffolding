using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace RandomSchoolAsync.Repositories
{
    public interface IRepositoryAsync<TEntity, TKey> where TEntity : class
    {
        TEntity GetItem(TKey id);
        IQueryable<TEntity> GetModelData(string[] includeModel, string filterData, string sortByExpression);
		IQueryable<TEntity> GetModelData(string filterData, string sortByExpression);

		Task<IQueryable> GetForeignListAsync(string foreignKeyModel, int keyType);

        Task<int> InsertItemAsync(ModelMethodContext modelMethodContext);
        Task<int> UpdateItemAsync(TKey id, ModelMethodContext modelMethodContext);
        Task<int> DeleteItemAsync(TKey id, ModelMethodContext modelMethodContext);    
	}
}
