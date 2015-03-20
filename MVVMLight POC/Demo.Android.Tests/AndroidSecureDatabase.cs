using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Demo.Shared;
using Demo.Shared.Models;
using SQLite;

namespace Demo.Android.Tests
{
    public class AndroidSecureDatabase : ISecureDatabase
    {
        private const string DatabaseName = "ProjectCompass.sqlite";
        private string encryptionPassword;

        public AndroidSecureDatabase()
        {
            Debug.WriteLine("Database Created");
        }

        public AndroidSecureDatabase(string encryptionPassword)
        {
            this.encryptionPassword = encryptionPassword;
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
			var db = new SQLiteConnection(GetDbPath(), encryptionPassword);
			#endif

            return db;
        }

        private string GetDbPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
        }
    }
}