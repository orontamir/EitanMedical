using Base.Models;

namespace IoTService.Interfaces
{
    public interface IIOService
    {
        Task<PatientsData?> ReadDataFromFileAsync(string filename);
    }
}
