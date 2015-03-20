using SQLite;

namespace Demo.Shared.Models
{
    public class Team : IIdentifiable
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CrestUrl { get; set; }
        public int SeasonId { get; set; }

        [PrimaryKey]
        public int Id { get; set; }
    }
}