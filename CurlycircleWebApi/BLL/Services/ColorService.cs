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
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ColorService(
            IColorRepository colorRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EntityCreatedViewModel> CreateColorAsync(ColorUpsertDto colorCreateDto)
        {
            var id = _colorRepository.AddColor(_mapper.Map<Color>(colorCreateDto));
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task DeleteColorAsync(int colorId)
        {
            await _colorRepository.DeleteColorAsync(colorId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ColorViewModel> FindColorByIdAsync(int colorId)
        {
            var color = await _colorRepository.GetColorByIdAsync(colorId);
            var colorViewModel = _mapper.Map<ColorViewModel>(color);
            return colorViewModel;
        }

        public async Task<ColorsViewModel> GetAllColorsAsync()
        {
            var colors = await _colorRepository.GetAllAsync();
            var colorsViewModel = _mapper.Map<ColorsViewModel>(colors);
            return colorsViewModel;
        }
    }
}
