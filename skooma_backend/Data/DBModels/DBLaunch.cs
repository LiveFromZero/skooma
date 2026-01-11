using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skooma_backend.Data.DBModels
{
    public class DBLaunch
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string RocketName { get; set; } = string.Empty;
        public DateTimeOffset LaunchDate { get; set; }

        [ForeignKey(nameof(DBLocation.LocationId))]
        public DBLocation Location { get; set; } = null!;
        public string Status { get; set; } = string.Empty;
    }
}
