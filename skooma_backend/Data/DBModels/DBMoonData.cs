using System.ComponentModel.DataAnnotations;

namespace skooma_backend.Data.DBModels
{
    public class DBMoonData
    {
        [Key]
        public int ID { get; set; }
        public int MoonPhase { get; set; }
        public DateTime Date { get; set; }
    }
}
