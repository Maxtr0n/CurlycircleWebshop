using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class PatternController : ApiController
    {

        private readonly IPatternService _patternService;

        public PatternController(IPatternService patternService)
        {
            _patternService = patternService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<EntityCreatedViewModel> CreatePattern([FromBody] PatternUpsertDto patternUpsertDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _patternService.CreatePatternAsync(patternUpsertDto);
        }

        [HttpGet]
        public Task<PatternsViewModel> GetPatterns()
        {
            return _patternService.GetAllPatternsAsync();
        }

        [HttpGet("{patternId}")]
        public Task<PatternViewModel> GetPatternById([FromRoute] int patternId)
        {
            return _patternService.FindPatternByIdAsync(patternId);
        }

        [HttpPut("{patternId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdatePattern([FromRoute] int patternId, [FromBody] PatternUpsertDto patternUpsertDto)
        {
            return _patternService.UpdatePatternAsync(patternId, patternUpsertDto);
        }

        [HttpDelete("{patternId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeletePattern([FromRoute] int patternId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _patternService.DeletePatternAsync(patternId);
        }
    }
}
