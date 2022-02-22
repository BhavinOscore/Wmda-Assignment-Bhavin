using System.Threading.Tasks;
using WMDAApi.ViewModels;
namespace WMDAApi.Services
{
    public interface ISearchMatchService
    {
        /// <summary>
        /// create search
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="matchEngineId"></param>
        /// <returns></returns>
        Task<ResponseModel> CreateSearchAsync(int patientId, int matchEngineId);
    }
}
