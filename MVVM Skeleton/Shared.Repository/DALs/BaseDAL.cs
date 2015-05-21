using System;
using Microsoft.Practices.Unity;
using CompassMobile.Shared.Common;
using System.Collections.Generic;
using SQLite;
using System.Linq;

namespace CompassMobile.Shared.DAL
{
	public abstract class BaseDAL<T> : IBaseDAL<T> where T: new()
	{
		private ISecureDatabase _database;

		protected ISecureDatabase Database
		{
			get {
				if (_database == null) {
					_database = IocContainer.GetContainer ().Resolve<ISecureDatabase> (); 
				}

				return _database;
			}
		}

		public BaseDAL()
		{
			Database.CreateTable<T> ();
		}

		public virtual IList<T> ReadAll()
		{
			try
			{
				var entities = Database.Query<T> ().ToList ();
				return entities;
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not read all {0}", typeof(T)),e);
			}
		}

		public virtual void Create (T entity)
		{
			try
			{
				Database.Insert (entity);
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not create {0}", typeof(T)),e);
			}
		}

		public virtual void CreateAll(IEnumerable<T> entities)
		{
			try
			{
				Database.InsertAll (entities);
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not create multiple {0}", typeof(T)),e);
			}
		}

		public virtual void CreateOrUpdate(T entity)
		{
			try
			{
				Database.InsertOrReplace (entity);
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not create or update {0}", typeof(T)),e);
			}
		}

		public virtual void CreateOrUpdateAll(IEnumerable<T> entities)
		{
			try
			{
				Database.InsertOrReplaceAll (entities);
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not create or update multiple {0}", typeof(T)),e);
			}

		}

		public virtual void Update (T entity)
		{
			try
			{
				Database.Update (entity);
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not update {0}", typeof(T)),e);
			}
		}

		public virtual void UpdateAll (IEnumerable<T> entities)
		{
			try
			{
				Database.UpdateAll (entities);
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not update {0}", typeof(T)),e);
			}
		}
			

		public void ClearTable()
		{
			try
			{
				Database.DeleteAll<T> ();
			}
			catch(SQLiteException e)
			{
				throw new DAException (string.Format("Could not clear table for {0}",typeof(T)),e);
			}
		}


		//TODO: keep adding model delete statements as you include additional models in the app
		//TODO: maybe a better alternative would be to delete the database file entirely
		protected void ClearAllDatabaseContent()
		{
			_database.CreateTable<Card> ();
			_database.CreateTable<CardAction> ();
			_database.CreateTable<MenuItem> ();
			_database.CreateTable<Member> ();

			_database.DeleteAll<Card> ();
			_database.DeleteAll<CardAction> ();
			_database.DeleteAll<MenuItem> ();
			_database.DeleteAll<Member> ();
		}
	}
}

