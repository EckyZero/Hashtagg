using System.Collections;
using System.Collections.Generic;
using Demo.Shared.Models;

namespace Demo.Shared
{
    public interface ISecureDatabase
    {
        int CreateTable<T>();
        void SeedTable<T>(); //TODO: May need to be removed as it was intended for demo data
        int Insert(object model);
        int InsertAll(IEnumerable models);
        int InsertOrReplace(object model);
        int InsertOrReplaceAll(IEnumerable models);
        int Delete<T>(T model) where T : IIdentifiable;
        int DeleteAll<T>();
        int Update(object model);
        int UpdateAll(IEnumerable models);
        T Query<T>(int id) where T : IIdentifiable, new();
        IEnumerable<T> Query<T>() where T : new();
        int ExecuteWithSql(string sql, params object[] bindings);
        IEnumerable<T> QueryWithSql<T>(string sql, params object[] bindings) where T : new();
    }
}