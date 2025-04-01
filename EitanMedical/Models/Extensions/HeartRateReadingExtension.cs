using Base.Models;
using EitanMedical.DAL.SQL.Entities;

namespace EitanMedical.Models.Extensions
{
    public static class HeartRateReadingExtension
    {
        public static HeartRateReadingEntity ToEntity(this HeartRateReading s)
        {
            if (s == null)
                return new HeartRateReadingEntity();
            return new HeartRateReadingEntity
            {
               
                HeartRate = s.HeartRate,
                PatientId = int.Parse(s.PatientId),
                Timestamp = s.Timestamp
               
            };
        }

        public static HeartRateReading ToModel(this HeartRateReadingEntity s)
        {

            return new HeartRateReading
            {
               
               HeartRate = s.HeartRate,
               PatientId = s.PatientId.ToString(),
               Timestamp = s.Timestamp

            };


        }
    }
}
