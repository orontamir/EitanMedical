using Base.Models;
using IoTService.Interfaces;
using System.Text.Json;

namespace IoTService.Services
{
    public class IOService: IIOService
    {
        public async Task<PatientsData?> ReadDataFromFileAsync(string filename)
        {
            using var reader = new StreamReader(filename);
            var json = await reader.ReadToEndAsync();
            var patientData = JsonSerializer.Deserialize<PatientsData>(json);

            return patientData;

        }
    }
}
