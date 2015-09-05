using System;
using Shared.Common;
using System.Collections;

namespace UnitTests
{
    public class MockDatabase : ISecureDatabase
    {
        #region ISecureDatabase implementation

        public int CreateTable<T>()
        {
            return 1;
        }

        public int Insert(object model)
        {
            return 1;
        }

        public int InsertAll(System.Collections.IEnumerable models)
        {
            var list = models as IList;
            return list.Count;
        }

        public int InsertOrReplace(object model)
        {
            return 1;
        }

        public int InsertOrReplaceAll(System.Collections.IEnumerable models)
        {
            var list = models as IList;
            return list.Count;
        }

        public int Delete<T>(T model) where T : IIdentifiable
        {
            return 1;
        }

        public int Delete<T>(T model, object primaryKey)
        {
            return 1;
        }

        public int DeleteAll<T>()
        {
            return 1;
        }

        public int Update(object model)
        {
            return 1;
        }

        public int UpdateAll(System.Collections.IEnumerable models)
        {
            var list = models as IList;
            return list.Count;
        }

        public T Query<T>(int id) where T : IIdentifiable, new()
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<T> Query<T>() where T : new()
        {
            return null;
        }

        public int ExecuteWithSql(string sql, params object[] bindings)
        {
            return 1;
        }

        public System.Collections.Generic.IEnumerable<T> QueryWithSql<T>(string sql, params object[] bindings) where T : new()
        {
            return null;
        }

        #endregion
    }
}

