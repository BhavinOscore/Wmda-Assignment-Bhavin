using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WMDAApi.Services;

namespace WMDAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchMatchController : ControllerBase
    {
        ISearchMatchService _searchMatchService;
        public SearchMatchController(ISearchMatchService service)
        {
            _searchMatchService = service;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateSearch(int patientId, int matchEngineId)
        {
            var model = await _searchMatchService.CreateSearchAsync(patientId, matchEngineId);
            return Ok(model);
        }
    }
}