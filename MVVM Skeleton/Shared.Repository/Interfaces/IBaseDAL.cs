using System;
using System.Collections.Generic;

namespace Shared.DAL
{
	public interface IBaseDAL<T>
	{
		IList<T> ReadAll ();
		void Create (T entity);
		void CreateAll (IEnumerable<T> entities);
		void CreateOrUpdate (T entity);
		void CreateOrUpdateAll (IEnumerable<T> entities);
		void Update (T entity);
		void UpdateAll (IEnumerable<T> entities);
		void ClearTable ();
        int Delete(T model);
        int DeleteSelected(List<T> models);
        int DeleteAll();
	}
}

