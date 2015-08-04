using System;
using System.Collections.Generic;

namespace Shared.Repo
{
	public interface IBaseRepo<T>
	{
		IList<T> ReadAll ();
		void Create (T entity);
		void CreateAll (IEnumerable<T> entities);
		void CreateOrUpdate (T entity);
		void CreateOrUpdateAll (IEnumerable<T> entities);
		void Update (T entity);
		void UpdateAll (IEnumerable<T> entities);
		void ClearAllDatabaseContent();
		void ClearTable ();
	}
}

