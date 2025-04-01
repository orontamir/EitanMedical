using Base.Models;

namespace EitanMedical.Interfaces
{
    public interface IPatientService
    {
        Task<bool> AddPatient(Patient model);
        Task<List<Patient>> GetAllPatients();
        Task<Patient?> GetPatientById(string id);
        Task<Dictionary<string, int>> GetAllPatientsRequest();
    }
}
