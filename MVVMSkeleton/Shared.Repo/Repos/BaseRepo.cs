using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Repo
{
	public abstract class BaseRepo<T> : IBaseRepo<T> where T : new ()
	{
		#region Private Variables

		protected ILogger _logger;
		private ISecureDatabase _database;

		#endregion

		#region Member Properties

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

		#endregion

		protected BaseRepo ()
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
				var exc = new RepoException(string.Format("Could not read all {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not create {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not create multiple {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not create or update {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not create or update multiple {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not update {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not update {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
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
				var exc = new RepoException(string.Format("Could not clear table for {0}", typeof(T)), e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
			}
		}

		public void ClearAllDatabaseContent()
		{
			try
			{
				// TODO: Add Tables
//				Database.CreateTable<CardAction>();

				// TODO: Remove Tables
//				Database.DeleteAll<CardAction>();
			}
			catch (Exception e)
			{
				var exc = new RepoException("Could not clear all database content", e);
				_logger.Log(exc, LogType.ERROR);
				throw exc;
			}
		}
	}
}

