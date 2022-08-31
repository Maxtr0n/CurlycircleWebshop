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
    public class PatternRepository : IPatternRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatternRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int AddPattern(Pattern pattern)
        {
            _dbContext.Patterns.Add(pattern);
            return pattern.Id;
        }

        public async Task DeletePatternAsync(int patternId)
        {
            var pattern = await _dbContext.Patterns.FindAsync(patternId);
            if (pattern != null)
            {
                _dbContext.Patterns.Remove(pattern);
            }
        }

        public async Task<IEnumerable<Pattern>> GetAllAsync()
        {
            var patterns = await _dbContext.Patterns
                .ToListAsync();
            return patterns;
        }

        public async Task<Pattern> GetPatternByIdAsync(int patternId)
        {
            var pattern = await _dbContext.Patterns
                .FirstOrDefaultAsync(o => o.Id == patternId);

            if (pattern == null)
            {
                throw new EntityNotFoundException($"Pattern with id {patternId} not found.");
            }

            return pattern;
        }

        public async Task<IEnumerable<Pattern>> GetPatternsByIdAsync(IEnumerable<int> patternIds)
        {
            List<Pattern> patterns = new();
            foreach (var patternId in patternIds)
            {
                var pattern = await _dbContext.Patterns.FindAsync(patternId);

                if (pattern == null)
                {
                    throw new EntityNotFoundException($"Pattern with id {patternId} not found.");
                }

                patterns.Add(pattern);
            }

            return patterns;
        }

        public void UpdatePattern(Pattern pattern)
        {
            _dbContext.Patterns.Update(pattern);
        }
    }
}
