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
    public class MaterialController : ApiController
    {

        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<EntityCreatedViewModel> CreateMaterial([FromBody] MaterialUpsertDto materialUpsertDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _materialService.CreateMaterialAsync(materialUpsertDto);
        }

        [HttpGet]
        public Task<MaterialsViewModel> GetMaterials()
        {
            return _materialService.GetAllMaterialsAsync();
        }

        [HttpGet("{materialId}")]
        public Task<MaterialViewModel> GetMaterialById([FromRoute] int materialId)
        {
            return _materialService.FindMaterialByIdAsync(materialId);
        }

        [HttpPut("{materialId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdateMaterial([FromRoute] int materialId, [FromBody] MaterialUpsertDto materialUpsertDto)
        {
            return _materialService.UpdateMaterialAsync(materialId, materialUpsertDto);
        }

        [HttpDelete("{materialId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteMaterial([FromRoute] int materialId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _materialService.DeleteMaterialAsync(materialId);
        }
    }
}
