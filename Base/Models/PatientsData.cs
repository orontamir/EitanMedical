using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Base.Models
{
    public class PatientsData
    {
        [JsonPropertyName("patients")]
        public List<Patient> Patients { get; set; }

        [JsonPropertyName("heartRateReadings")]
        public List<HeartRateReading> HeartRateReadings { get; set; }
    }
}
