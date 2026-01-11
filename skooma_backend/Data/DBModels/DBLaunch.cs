using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skooma_backend.Data.DBModels
{
    public class DBLaunch
    {
        [Key]
        public int Id { get; set; }
        public string RocketName { get; set; } = string.Empty;
        public DateTimeOffset LaunchDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public int LocationId { get; set; }

        [ForeignKey(nameof(DBLocation.LocationId))]
        public DBLocation Location { get; set; } = null!;
    }
}
