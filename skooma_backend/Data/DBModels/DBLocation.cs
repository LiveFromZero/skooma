using System.ComponentModel.DataAnnotations;

namespace skooma_backend.Data.DBModels
{
    public class DBLocation
    {
        [Key]
        public int LocationId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;

        public ICollection<DBLaunch> Launches {get; set;} = new List<DBLaunch>();
    }
}
