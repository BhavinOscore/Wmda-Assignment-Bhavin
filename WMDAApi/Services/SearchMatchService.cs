using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WMDAApi.Models;
using WMDAApi.Utils;
using WMDAApi.ViewModels;
namespace WMDAApi.Services
{
    public class SearchMatchService : ServiceBase, ISearchMatchService
    {
        public SearchMatchService(IAppDbContext context) : base(context)
        {
        }
        public async Task<ResponseModel> CreateSearchAsync(int patientId, int matchEngineId)
        {
            ResponseModel model = new ResponseModel();
            
            var errors = new List<ErrorData>();
            MatchEngine _matchEngine = MatchEngineManager.MatchEngines.Where(x => x.MatchEngineId == matchEngineId).FirstOrDefault();
            if (_matchEngine == null)
            {
                errors.Add(new ErrorData() { Message = "Match Engine Not Found." });
            }
            Patient _patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.PatientId == patientId);
            if (_patient == null)
            {
                errors.Add(new ErrorData() { Message = "Patient Not Found." });
            }
            if (_matchEngine != null && _patient != null)
            {
                SendToCorrectAPI(model, _matchEngine, _patient, errors);
            }
            if(errors.Count > 0)
                throw new CustomException(errors);
            return model;
        }

        private void SendToCorrectAPI(ResponseModel model, MatchEngine _matchEngine, Patient _patient, List<ErrorData> errors)
        {
            using (var client = new HttpClient())
            {
                //HTTP POST
                MatchEnginePatientFactory factory = null;

                factory = GetFactory(_matchEngine, _patient);
                
                MatchEnginePatient _matchEnginePatient = factory.GetMatchEnginePatient();

                var json = JsonConvert.SerializeObject(new { patient = _matchEnginePatient });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(_matchEngine.EndPoint.Trim('/') + "/post", data).Result;

                if (response.IsSuccessStatusCode)
                {
                    model.IsSuccess = true;
                    model.Messsage = "Sent to correct API endpoint successfully";
                }
                else
                {
                    errors.Add(new ErrorData() { Message = $"Error while posting data to match engine. Status code = {response.StatusCode}", Code = response.StatusCode.ToString()});
                }
            }
        }

        private static MatchEnginePatientFactory GetFactory(MatchEngine _matchEngine, Patient _patient)
        {
            MatchEnginePatientFactory factory = null;
            switch (_matchEngine.MatchEngineId)
            {
                case 1:
                    factory = new MatchEngineOnePatientFactory(_patient);
                    break;
                case 2:
                    factory = new MatchEngineTwoPatientFactory(_patient);
                    break;
                default:
                    break;
            }

            return factory;
        }
    }
}
