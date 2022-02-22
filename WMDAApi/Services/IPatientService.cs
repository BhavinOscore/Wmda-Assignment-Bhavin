using System.Threading.Tasks;
using WMDAApi.Models;
using WMDAApi.ViewModels;

namespace WMDAApi.Services
{
    public interface IPatientService
    {
        /// <summary>
        ///  add edit Patient
        /// </summary>
        /// <param name="patientModel"></param>
        /// <returns></returns>
        Task<ResponseModel> CreatePatientAsync(Patient patientModel);
    }
}
