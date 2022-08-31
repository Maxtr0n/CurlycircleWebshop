using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MaterialRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int AddMaterial(Material material)
        {
            _dbContext.Materials.Add(material);
            return material.Id;
        }

        public async Task DeleteMaterialAsync(int materialId)
        {
            var material = await _dbContext.Materials.FindAsync(materialId);
            if (material != null)
            {
                _dbContext.Materials.Remove(material);
            }
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            var materials = await _dbContext.Materials
                .ToListAsync();
            return materials;
        }

        public async Task<Material> GetMaterialByIdAsync(int materialId)
        {
            var material = await _dbContext.Materials
                .FirstOrDefaultAsync(o => o.Id == materialId);

            if (material == null)
            {
                throw new EntityNotFoundException($"Material with id {materialId} not found.");
            }

            return material;
        }

        public async Task<IEnumerable<Material>> GetMaterialsByIdAsync(IEnumerable<int> materialIds)
        {
            List<Material> materials = new();
            foreach (var materialId in materialIds)
            {
                var material = await _dbContext.Materials.FindAsync(materialId);

                if (material == null)
                {
                    throw new EntityNotFoundException($"Material with id {materialId} not found.");
                }

                materials.Add(material);
            }

            return materials;
        }

        public void UpdateMaterial(Material material)
        {
            _dbContext.Materials.Update(material);
        }
    }
}
