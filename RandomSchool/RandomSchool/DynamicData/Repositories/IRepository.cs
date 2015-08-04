using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace RandomSchool.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
		TEntity GetItem(TKey id);
        IQueryable<TEntity> GetModelData(string[] includeModel, string filterData, string sortByExpression);
		IQueryable<TEntity> GetModelData(string filterData, string sortByExpression);

        IQueryable GetForeignList(string foreignKeyModel, int keyType);

        int InsertItem(ModelMethodContext modelMethodContext);
        int UpdateItemBase(TKey id, ModelMethodContext modelMethodContext);
        int DeleteItemBase(TKey id, ModelMethodContext modelMethodContext);
    }
}
