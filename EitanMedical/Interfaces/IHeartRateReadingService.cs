using Base.Models;
using EitanMedical.DAL.SQL.Entities;

namespace EitanMedical.Interfaces
{
    public interface IHeartRateReadingService
    {
        Task<bool> AddHeartRateReading(HeartRateReading model);
        Task<List<HeartRateReading>> GetAllHeartRateReadings();
        Task<bool> UpdatePatientRequest (string id);
    }
}
