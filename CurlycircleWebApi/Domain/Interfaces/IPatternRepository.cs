using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPatternRepository
    {
        int AddPattern(Pattern pattern);

        Task<IEnumerable<Pattern>> GetAllAsync();

        Task<Pattern> GetPatternByIdAsync(int patternId);

        Task<IEnumerable<Pattern>> GetPatternsByIdAsync(IEnumerable<int> patternIds);

        void UpdatePattern(Pattern pattern);

        Task DeletePatternAsync(int patternId);
    }
}
