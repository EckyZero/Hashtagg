using System;
using SQLite;

namespace Demo.Shared.Models
{
    public class Season : IIdentifiable
    {
        public string Caption { get; set; }
        public string League { get; set; }
        public string Year { get; set; }
        public DateTime LastUpdated { get; set; }

        [PrimaryKey]
        public int Id { get; set; }
    }
}