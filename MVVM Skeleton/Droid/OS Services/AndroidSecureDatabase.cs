using System;
using Droid;
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
		private const string DatabaseName = "ProjectCompass.sqlite";

		//TODO: revisit
		//Note: Android has no KeyChain services equivalent. Using a key by obfuscation
		private readonly string _1sds0292ds2_32ldjs_almd_o = "^t%B(&3W{23$H]CHPd=t2kHFMh66y";
		private readonly string _2ndsv_dmsal92020ilsm9_282sd = "BW;47O0GQ167v4,f]qR,IAqRA83Ucx";
		private readonly string _289ms9_282md_28dm82_22323 = "b]U42+{]#4$8o15|ST9b?85M0+pCzt";
		private readonly string _ksjkl_2923_239dm_hgl29023 = "qW~D3&Q}|U12wZE6^RX/ppcc]-68Z";

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
					//TODO: may need to check if database connection is still open. However, there's no method that 
					// has furnishes that feature at the SQLITE.NET level. We may need to figure this out using some other way (if at all needed)
					return _database;
				} else {
					_database = CreateSqlConnection ();
					return _database;
				}
			}
		}

		private SQLiteConnection CreateSqlConnection()
		{
			#if DEBUG
			var db = new SQLiteConnection(GetDbPath()); //no password for debugging
			#else
			var db = new SQLiteConnection (GetDbPath (), string.Format ("{0}{1}{2}{3}", _1sds0292ds2_32ldjs_almd_o, _289ms9_282md_28dm82_22323, _2ndsv_dmsal92020ilsm9_282sd, _ksjkl_2923_239dm_hgl29023));
			#endif

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

