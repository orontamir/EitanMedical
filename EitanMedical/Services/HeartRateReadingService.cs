using Base.Models;
using Base.Services;
using EitanMedical.DAL.SQL;
using EitanMedical.DAL.SQL.Entities;
using EitanMedical.Interfaces;
using EitanMedical.Models.Extensions;
using EitanMedical.Services;

namespace IoTService.Services
{
    public class HeartRateReadingService: DALService, IHeartRateReadingService
    {
        public HeartRateReadingService(RepositoryBase repo) : base(repo)
        {
        }

        public async Task<bool> AddHeartRateReading(HeartRateReading model)
        {
            try
            {
                //Checking if patient id is exist
                PatientEntity? patientEnity = await Repository.GetPatientById(model.PatientId);
                if (patientEnity != null)
                {
                    //if yes, adding Heart Rate Reading into the table and update Request in patient table
                    HeartRateReadingEntity messageentity = await Repository.AddHeartRateReading(model.ToEntity());
                    return true;
                }
                else 
                {
                    //if no, throw new exception and return false
                    LogService.LogError($"patient id : {model.PatientId} not exist");
                    return false;
                }


            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when add Hear Rate Reading , error message: {ex.Message}");
                return false;
            }
        }

        public Task<List<HeartRateReading>> GetAllHeartRateReadings()
        {
            return Task.FromResult(Repository.GetAllHeartRateReadings().Result.Select(o => o.ToModel()).ToList());
        }

        public async Task<bool> UpdatePatientRequest(string id)
        {
            try
            {
                //Get Patient entity by id
                var patient = Repository.GetPatientById(id);
                if (patient != null && patient.Result != null)
                {
                    //Update patient request counter +1
                    patient.Result.Request += 1;
                    await Repository.UpdatePatientRequest(patient.Result);
                    return true;
                }
                else
                {
                    LogService.LogError($"Exception when update patient request , patient is null");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when update patient request , error message: {ex.Message}");
                return false;
            }
            


        }
    }
}
