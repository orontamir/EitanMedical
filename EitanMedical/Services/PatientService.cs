using Base.Models;
using Base.Services;
using EitanMedical.DAL.SQL;
using EitanMedical.DAL.SQL.Entities;
using EitanMedical.Interfaces;
using EitanMedical.Models.Extensions;
using EitanMedical.Services;

namespace IoTService.Services
{
    public class PatientService: DALService, IPatientService
    {
        public PatientService(RepositoryBase repo) : base(repo)
        {
        }

        public async Task<bool> AddPatient(Patient model)
        {
            try
            {
                //Checking if patient id is exist
                PatientEntity? patientEnity = await Repository.GetPatientById(model.Id);
                if (patientEnity == null)
                {
                    //if no, adding patient into patient table
                    PatientEntity messageentity = await Repository.AddPatient(model.ToEntity());
                    return true;
                }
                else 
                {
                    //if yes, throw new exception and return false
                    LogService.LogError($"Patient: {model.Name} already exist");
                    return false;
                }



            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when add Hear Rate Reading , error message: {ex.Message}");
                return false;
            }
        }

        public  Task<List<Patient>> GetAllPatients()
        {
            return Task.FromResult(Repository.GetAllPatients().Result.Select(o => o.ToModel()).ToList());
        }

        public Task<Dictionary<string, int>> GetAllPatientsRequest()
        {
            Dictionary<string, int> PatientRequestDict = new Dictionary<string, int>();
            var patients = Repository.GetAllPatients().Result;
            foreach (var patient in patients) 
            {
                PatientRequestDict.Add(patient.Name, patient.Request);
            }
            return Task.FromResult(PatientRequestDict);
        }

        public  Task<Patient?> GetPatientById(string id)
        {
            return Task.FromResult (Repository.GetPatientById(id).Result?.ToModel());
        }
    }
}
