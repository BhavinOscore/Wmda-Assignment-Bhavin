using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WMDAApi.Models;
using WMDAApi.Services;

namespace WMDAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientService _patientService;
        public PatientController(IPatientService service)
        {
            _patientService = service;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreatePatient(Patient patientModel)
        {
            var model = await _patientService.CreatePatientAsync(patientModel);
            return Ok(model);
        }
    }
}
