using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SQLite;
using Java.Security;
using Shared.Common;

namespace Droid
{
	public class AndroidSecureDatabase : BaseService, ISecureDatabase
	{
		private const string DatabaseName = "Sqlite";

		private object _lock = new object ();

		private SQLiteConnection _database;
		SQLiteConnection Database { 
			get {
				return GetDb ();
			} 
		}

		public AndroidSecureDatabase()
		{
			System.Diagnostics.Debug.WriteLine("Database Created");
		}

		public int CreateTable<T>()
		{
			return Database.CreateTable<T>();
		}

		public int Insert(object model)
		{
			return Database.Insert(model);
		}


		public int InsertAll(IEnumerable models)
		{
			return Database.InsertAll(models);
		}

		public int InsertOrReplace(object model)
		{
			return Database.InsertOrReplace(model);
		}

		public int InsertOrReplaceAll(IEnumerable models)
		{
			int updatedRowsCount = 0;
			Database.RunInTransaction(() =>
				{
					foreach (object model in models)
					{
						updatedRowsCount += Database.InsertOrReplace(model);
					}
				});

			return updatedRowsCount;
		}

		public int Delete<T>(T model, object primaryKey)
		{
			return Database.Delete<T>(primaryKey);
		}

		public int Delete<T>(T model) where T : IIdentifiable
		{
			return Database.Delete<T>(model.Id);
		}

		public int DeleteAll<T>()
		{
			return Database.DeleteAll<T>();
		}

		public int Update(object model)
		{
			return Database.Update(model);
		}

		public int UpdateAll(IEnumerable models)
		{
			return Database.UpdateAll(models);
		}

		public T Query<T>(int id) where T : IIdentifiable, new()
		{
			return Database.Table<T>().FirstOrDefault(item => item.Id == id);
		}

		public IEnumerable<T> Query<T>() where T : new()
		{
			return Database.Table<T>().ToList();
		}

		public IEnumerable<T> QueryWithSql<T>(string sql, params object[] bindings) where T : new()
		{
			return Database.Query<T>(sql, bindings);
		}

		public int ExecuteWithSql(string sql, params object[] bindings)
		{
			return Database.Execute(sql, bindings);
		}

		public void SeedTable<T>()
		{
			throw new NotImplementedException();
		}

		private SQLiteConnection GetDb()
		{
			lock (_lock) {

				if (_database != null) {
					return _database;
				} else {
					_database = CreateSqlConnection ();
					return _database;
				}
			}
		}

		private SQLiteConnection CreateSqlConnection()
		{
			var db = new SQLiteConnection(GetDbPath()); 
			
			return db;
		}

		private string GetDbPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
		}

	    public static void DeleteDatabase()
	    {
            var filePath =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);

	        if(File.Exists(filePath))
                File.Delete(filePath);
	    }
	}
}

