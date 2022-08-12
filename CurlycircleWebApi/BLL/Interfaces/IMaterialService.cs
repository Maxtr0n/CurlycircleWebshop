using BLL.Dtos;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMaterialService
    {
        Task<EntityCreatedViewModel> CreateMaterialAsync(MaterialUpsertDto materialCreateDto);

        Task<MaterialsViewModel> GetAllMaterialsAsync();

        Task<MaterialViewModel> FindMaterialByIdAsync(int materialId);

        Task UpdateMaterialAsync(int materialId, MaterialUpsertDto materialUpdateDto);

        Task DeleteMaterialAsync(int materialId);
    }
}
