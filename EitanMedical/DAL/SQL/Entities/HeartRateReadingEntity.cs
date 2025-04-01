using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EitanMedical.DAL.SQL.Entities
{
    [Table("HeartRateReadings")]
    public class HeartRateReadingEntity
    {
       
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // DB generated -- unique in all DB

        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("heart_rate")]
        public int HeartRate { get; set; }
    }
}
