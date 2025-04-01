using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Base.Models
{
    public class HeartRateReading
    {
        [JsonPropertyName("patientId")]
        public string PatientId { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("heartRate")]
        public int HeartRate { get; set; }
    }
}
