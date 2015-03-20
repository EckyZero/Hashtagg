using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Common;
using SQLite;

namespace Droid
{
	public class AndroidSecureDatabase : ISecureDatabase
	{
        public int Delete<T>(T model, object primaryKey)
        {
            using (SQLiteConnection db = GetDb())
            {
                return db.Delete<T>(primaryKey);
            }
        }

		private const string DatabaseName = "ProjectCompass.sqlite";

		//TODO: revisit
		//Note: Android has no KeyChain services equivalent. Using a key by obfuscation
		private readonly string _1sds0292ds2_32ldjs_almd_o = "^t%B(&3W{23$H]CHPd=t2kHFMh66y";
		private readonly string _2ndsv_dmsal92020ilsm9_282sd = "BW;47O0GQ167v4,f]qR,IAqRA83Ucx";
		private readonly string _289ms9_282md_28dm82_22323 = "b]U42+{]#4$8o15|ST9b?85M0+pCzt";
		private readonly string _ksjkl_2923_239dm_hgl29023 = "qW~D3&Q}|U12wZE6^RX/ppcc]-68Z";


		public AndroidSecureDatabase()
		{
			System.Diagnostics.Debug.WriteLine("Database Created");
		}

		public int CreateTable<T>()
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.CreateTable<T>();
			}
		}

		public int Insert(object model)
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Insert(model);
			}
		}


		public int InsertAll(IEnumerable models)
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.InsertAll(models);
			}
		}

		public int InsertOrReplace(object model)
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.InsertOrReplace(model);
			}
		}

		public int InsertOrReplaceAll(IEnumerable models)
		{
			int updatedRowsCount = 0;
			using (SQLiteConnection db = GetDb())
			{
				db.RunInTransaction(() =>
					{
						foreach (object model in models)
						{
							updatedRowsCount += db.InsertOrReplace(model);
						}
					});
			}

			return updatedRowsCount;
		}

		public int Delete<T>(T model) where T : IIdentifiable
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Delete<T>(model.Id);
			}
		}

		public int DeleteAll<T>()
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.DeleteAll<T>();
			}
		}

		public int Update(object model)
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Update(model);
			}
		}

		public int UpdateAll(IEnumerable models)
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.UpdateAll(models);
			}
		}

		public T Query<T>(int id) where T : IIdentifiable, new()
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Table<T>().FirstOrDefault(item => item.Id == id);
			}
		}

		public IEnumerable<T> Query<T>() where T : new()
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Table<T>().ToList();
			}
		}

		public IEnumerable<T> QueryWithSql<T>(string sql, params object[] bindings) where T : new()
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Query<T>(sql, bindings);
			}
		}

		public int ExecuteWithSql(string sql, params object[] bindings)
		{
			using (SQLiteConnection db = GetDb())
			{
				return db.Execute(sql, bindings);
			}
		}

		public void SeedTable<T>()
		{
			throw new NotImplementedException();
		}

		private SQLiteConnection GetDb()
		{
			#if DEBUG
			var db = new SQLiteConnection(GetDbPath()); //no password for debugging
			#else
			var db = new SQLiteConnection(GetDbPath(), string.Format("{0}{1}{2}{3}",_1sds0292ds2_32ldjs_almd_o,_289ms9_282md_28dm82_22323,_2ndsv_dmsal92020ilsm9_282sd,_ksjkl_2923_239dm_hgl29023));
			#endif

			return db;
		}

		private string GetDbPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
		}
	}
}

