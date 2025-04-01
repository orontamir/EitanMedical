using Base.Models;

namespace IoTService.Interfaces
{
    public interface IIotService
    {
        Task<bool> SendAllPatients(List<Patient> patients);

        Task<bool> SendAllHeartRateReadings(List<HeartRateReading> heartRateReadings);

        Task<bool> SendPatient(Patient patient);

        Task<bool> SendHeartRateReading(HeartRateReading heartRateReading);
    }
}
