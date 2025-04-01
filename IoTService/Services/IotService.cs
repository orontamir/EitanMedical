using Base.Models;
using Base.Services;
using IoTService.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace IoTService.Services
{
    public class IotService: IIotService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpService _httpService;
        private readonly string _patientUrl;
        private readonly string _heartRateReadingUrl;

        public IotService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpService = new HttpService(new HttpClient());
            _httpService.BaseAddress = _configuration.GetValue<string?>("EitanMedicalUrl");

        }

        public async Task<bool> SendAllHeartRateReadings(List<HeartRateReading> heartRateReadings)
        {
            try
            {
                var url = _configuration.GetValue<string?>("Communication:HeartRateReading:API:SendAllHeartRateReadings") ?? throw new Exception("No API url exist");
                LogService.LogInformation($"url = {url}"); 
                await _httpService.PostAsync<string>(url, new System.Collections.Generic.Dictionary<string, object>(), heartRateReadings);
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when sending all Heart Rate Readings, error message: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendAllPatients(List<Patient> patients) 
        {
            try
            {
                var url = _configuration.GetValue<string?>("Communication:Patient:API:SendAllPatients") ?? throw new Exception("No API url exist");
                LogService.LogInformation($"url = {url}");
                await _httpService.PostAsync<string>(url, new System.Collections.Generic.Dictionary<string, object>(), patients);
                return true;
            }
            catch (Exception ex) 
            {
                LogService.LogError($"Exception when sending all patients, error message: {ex.Message}");
                return false;
            }
         
        }

        public async Task<bool> SendHeartRateReading(HeartRateReading heartRateReading)
        {
            try
            {
                var url = _configuration.GetValue<string?>("Communication:HeartRateReading:API:HeartRateReading") ?? throw new Exception("No API url exist");
               
                await _httpService.PostAsync<string>(url, new System.Collections.Generic.Dictionary<string, object>(), heartRateReading);
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when sending all patients, error message: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendPatient(Patient patient)
        {
            try
            {
                var url = _configuration.GetValue<string?>("Communication:Patient:API:SendPatient") ?? throw new Exception("No API url exist");
                await _httpService.PostAsync<string>(url, new System.Collections.Generic.Dictionary<string, object>(), patient);
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when sending all patients, error message: {ex.Message}");
                return false;
            }
        }
    }
}
