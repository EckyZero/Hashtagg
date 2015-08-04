using System;
using Shared.Common;
using SQLite;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Practices.Unity;
using System.Security.Cryptography;
using System.Text;

namespace iOS
{
    public class iOSSecureDatabase : BaseService, ISecureDatabase
	{
		private const string _databaseName = "ProjectCompass.sqlite";
		private const string _keyName = "SqlCipher"; 
		private object _lock = new object ();

		public iOSSecureDatabase()
		{
			System.Diagnostics.Debug.WriteLine("Database Created");
		}

		private SQLiteConnection _database;
		SQLiteConnection Database { 
			get {
				return GetDb ();
			} 
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

		public int Delete<T>(T model) where T : IIdentifiable
		{
			return Database.Delete<T>(model.Id);
		}

		public int Delete<T> (T model, object primaryKey)
		{
			return Database.Delete<T>(primaryKey);
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
			var keystore = IocContainer.GetContainer().Resolve<ISecureKeyValueStore>();
			var encryptionKey = keystore.Retrieve(_keyName);

			if(encryptionKey == null){
				//generate encryption key and store in db
				AesManaged aes = new AesManaged();
				aes.GenerateKey();
				var key = aes.Key;

				keystore.Store( _keyName,Encoding.Default.GetString(key) );
				encryptionKey = Encoding.Default.GetString(key);
			}

			var db = new SQLiteConnection(GetDbPath(), encryptionKey);		
			#endif

			return db;
		}

		private string GetDbPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);
		}

        public static void DeleteDatabase()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
	}
}

