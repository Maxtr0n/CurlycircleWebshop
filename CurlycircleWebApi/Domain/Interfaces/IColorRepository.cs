using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IColorRepository
    {
        int AddColor(Color color);

        Task<IEnumerable<Color>> GetAllAsync();

        Task<Color> GetColorByIdAsync(int colorId);

        Task<IEnumerable<Color>> GetColorsByIdsAsync(IEnumerable<int> colorIds);

        void UpdateColor(Color color);

        Task DeleteColorAsync(int colorId);
    }
}
