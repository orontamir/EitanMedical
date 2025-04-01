
using Base.Models;
using Base.Services;
using IoTService.Interfaces;

namespace IoTService.Services
{
    public class MainService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IIOService _ioservice;
        private readonly IIotService _iotService;
        private static bool _processIsRunning = false;
        public MainService(IConfiguration configuration, IIOService ioservice, IIotService iotService)
        {
            _configuration = configuration;
            _ioservice = ioservice;
            _iotService = iotService;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _processIsRunning = true;
           
            try
            {
                string? filePath = !string.IsNullOrEmpty(_configuration.GetSection("PathFile").Value)  ? _configuration.GetSection("PathFile").Value : "C:\\Patients";
                while (_processIsRunning)
                {

                    try
                    {
                        //Read All files from the folder 
                        foreach (string filename in Directory.EnumerateFiles(filePath, "*.json"))
                        {
                            //Read all patient data from json file
                            FileInfo fileInfo = new FileInfo(filename);
                            string fullFilePath = fileInfo.FullName;
                            PatientsData? data = await _ioservice.ReadDataFromFileAsync(fullFilePath);
                            if (data != null)
                            {
                                //Send all patiens to Eitan Medial service
                                bool isSendingPatients =  await _iotService.SendAllPatients(data.Patients);
                                if (!isSendingPatients) 
                                {
                                    LogService.LogError($"There was an exception when sending all patients");
                                }
                                //Send all Heart Rate Reading
                                bool isSendingHeartRateReadings = await _iotService.SendAllHeartRateReadings(data.HeartRateReadings);
                                if (!isSendingHeartRateReadings)
                                {
                                    LogService.LogError($"There was an exception when sending all Heart Rate Readings");
                                }
                            }
                            else 
                            {
                                LogService.LogError($"No Data exist on json file: {filename}");
                            }

                            if (File.Exists(filename))
                            {
                                //Delete file after finish read it
                                File.Delete(filename);

                            }
                        }

                        Thread.Sleep(1000);
                    }
                    catch (Exception ex) { LogService.LogError($"Error messahe: {ex.Message}"); }
                }
            }
            catch (Exception ex) { LogService.LogError($"Error messahe: {ex.Message}"); }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
