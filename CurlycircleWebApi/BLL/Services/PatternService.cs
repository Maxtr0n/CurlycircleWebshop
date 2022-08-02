using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PatternService : IPatternService
    {
        private readonly IPatternRepository _patternRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatternService(
            IPatternRepository patternRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _patternRepository = patternRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EntityCreatedViewModel> CreatePatternAsync(PatternUpsertDto patternCreateDto)
        {
            var id = _patternRepository.AddPattern(_mapper.Map<Pattern>(patternCreateDto));
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task DeletePatternAsync(int patternId)
        {
            await _patternRepository.DeletePatternAsync(patternId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PatternViewModel> FindPatternByIdAsync(int patternId)
        {
            var pattern = await _patternRepository.GetPatternByIdAsync(patternId);
            var patternViewModel = _mapper.Map<PatternViewModel>(pattern);
            return patternViewModel;
        }

        public async Task<IEnumerable<PatternViewModel>> GetAllPatternsAsync()
        {
            var patterns = await _patternRepository.GetAllAsync();
            var patternsViewModel = _mapper.Map<IEnumerable<PatternViewModel>>(patterns);
            return patternsViewModel;
        }

        public async Task UpdatePatternAsync(int patternId, PatternUpsertDto patternUpdateDto)
        {
            _patternRepository.UpdatePattern(_mapper.Map<Pattern>(patternUpdateDto));
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
