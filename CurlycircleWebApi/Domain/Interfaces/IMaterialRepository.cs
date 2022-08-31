using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMaterialRepository
    {
        int AddMaterial(Material material);

        Task<IEnumerable<Material>> GetAllAsync();

        Task<Material> GetMaterialByIdAsync(int materialId);

        Task<IEnumerable<Material>> GetMaterialsByIdAsync(IEnumerable<int> materialIds);

        void UpdateMaterial(Material material);

        Task DeleteMaterialAsync(int materialId);
    }
}
