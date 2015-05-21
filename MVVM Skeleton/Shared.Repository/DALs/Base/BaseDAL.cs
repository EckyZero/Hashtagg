using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Common;
using Shared.Common.Logging;
using Shared.DAL;

namespace Shared.Repository.DALs.Base
{
    public abstract class BaseDAL<T> : IBaseDAL<T> where T : IIdentifiable, new()
	{
	    protected ILogger _logger;

		private ISecureDatabase _database;

		protected ISecureDatabase Database
		{
			get {
				if (_database == null) {
					_database = IocContainer.GetContainer ().Resolve<ISecureDatabase> ();

                    _database.CreateTable<T>();
				}
				return _database;
			}
		}

		public BaseDAL()
		{
		    _logger = IocContainer.GetContainer().Resolve<ILogger>();
		}

		public virtual IList<T> ReadAll()
		{
			try
			{
				var entities = Database.Query<T> ().ToList ();
				return entities;
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}

		public virtual void Create (T entity)
		{
			try
			{
				Database.Insert (entity);
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}

		public virtual void CreateAll(IEnumerable<T> entities)
		{
			try
			{
				Database.InsertAll (entities);
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}

		public virtual void CreateOrUpdate(T entity)
		{
			try
			{
				Database.InsertOrReplace (entity);
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}

		public virtual void CreateOrUpdateAll(IEnumerable<T> entities)
		{
			try
			{
				Database.InsertOrReplaceAll (entities);
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }

		}

		public virtual void Update (T entity)
		{
			try
			{
				Database.Update (entity);
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}

		public virtual void UpdateAll (IEnumerable<T> entities)
		{
			try
			{
				Database.UpdateAll (entities);
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}
			

		public void ClearTable()
		{
			try
			{
				Database.DeleteAll<T> ();
			}
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
		}

        public int Delete(T model)
        {
            try
            {
                return Database.Delete(model);
            }
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
        }

        public int DeleteSelected(List<T> models)
        {
            try
            {
                var count = 0;

                foreach (T model in models)
                {
                    count += Delete(model);
                }
                return count;
            }
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
        }

        public int DeleteAll()
        {
            try
            {
                return Database.DeleteAll<T>();
            }
            catch (Exception e)
            {
                _logger.Log(e, LogType.ERROR);
                throw;
            }
        }
	}
}

