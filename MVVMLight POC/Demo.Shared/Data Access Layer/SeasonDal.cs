using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Shared.Exceptions;
using Demo.Shared.Models;
using Xamarin;
using SQLite;

namespace Demo.Shared
{
    public static class SeasonDal
    {
        static SeasonDal()
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            try
            {
                database.CreateTable<Season>();
            }
            catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException("Creating table Season failed.", exception);
            }
        }

        public static List<Season> GetAll()
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            try
            {
                return database.Query<Season>().ToList();
            }
			catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException("Getting all seasons from database failed.", exception);
            }
        }

        public static void DeleteAll()
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            try
            {
                database.DeleteAll<Season>();
            }
			catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException("Deleting all seasons from database failed.", exception);
            }
        }

        public static void Refresh(List<Season> seasons)
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            try
            {
                DeleteAll();
                database.InsertAll(seasons);
            }
            catch (Exception exception)
            {
                Insights.Report(exception);
                throw new DalException("Replacing all seasons in database failed.", exception);
            }
        }
    }
}