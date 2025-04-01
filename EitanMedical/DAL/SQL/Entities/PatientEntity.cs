using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EitanMedical.DAL.SQL.Entities
{
    [Table("Patients")]
    public class PatientEntity
    {
       
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // DB generated -- unique in all DB

        [Column("name")]
        public string Name { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("request")]
        public int Request { get; set; }
    }
}
