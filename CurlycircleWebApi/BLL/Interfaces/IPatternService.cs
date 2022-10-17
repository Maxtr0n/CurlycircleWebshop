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

        Task<PatternsViewModel> GetAllPatternsAsync();

        Task<PatternViewModel> FindPatternByIdAsync(int patternId);

        Task DeletePatternAsync(int patternId);
    }
}
