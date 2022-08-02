using BLL.Dtos;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPatternService
    {
        Task<EntityCreatedViewModel> CreatePatternAsync(PatternUpsertDto patternCreateDto);

        Task<IEnumerable<PatternViewModel>> GetAllPatternsAsync();

        Task<PatternViewModel> FindPatternByIdAsync(int patternId);

        Task UpdatePatternAsync(int patternId, PatternUpsertDto patternUpdateDto);

        Task DeletePatternAsync(int patternId);
    }
}
