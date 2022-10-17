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
    public class ColorController : ApiController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<EntityCreatedViewModel> CreateColor([FromBody] ColorUpsertDto colorUpsertDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _colorService.CreateColorAsync(colorUpsertDto);
        }

        [HttpGet]
        public Task<ColorsViewModel> GetColors()
        {
            return _colorService.GetAllColorsAsync();
        }

        [HttpGet("{colorId}")]
        public Task<ColorViewModel> GetColorById([FromRoute] int colorId)
        {
            return _colorService.FindColorByIdAsync(colorId);
        }

        [HttpDelete("{colorId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteColor([FromRoute] int colorId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _colorService.DeleteColorAsync(colorId);
        }
    }

}
