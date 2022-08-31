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
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ColorRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int AddColor(Color color)
        {
            _dbContext.Colors.Add(color);
            return color.Id;
        }

        public async Task DeleteColorAsync(int colorId)
        {
            var color = await _dbContext.Colors.FindAsync(colorId);
            if (color != null)
            {
                _dbContext.Colors.Remove(color);
            }
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            var colors = await _dbContext.Colors
                .ToListAsync();
            return colors;
        }

        public async Task<Color> GetColorByIdAsync(int colorId)
        {
            var color = await _dbContext.Colors
                .FirstOrDefaultAsync(o => o.Id == colorId);

            if (color == null)
            {
                throw new EntityNotFoundException($"Color with id {colorId} not found.");
            }

            return color;
        }

        public async Task<IEnumerable<Color>> GetColorsByIdsAsync(IEnumerable<int> colorIds)
        {
            List<Color> colors = new();
            foreach (var colorId in colorIds)
            {
                var color = await _dbContext.Colors.FindAsync(colorId);

                if (color == null)
                {
                    throw new EntityNotFoundException($"Color with id {colorId} not found.");
                }

                colors.Add(color);
            }

            return colors;
        }

        public void UpdateColor(Color color)
        {
            _dbContext.Colors.Update(color);
        }
    }
}
