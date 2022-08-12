using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialService(
            IMaterialRepository materialRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _materialRepository = materialRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EntityCreatedViewModel> CreateMaterialAsync(MaterialUpsertDto materialCreateDto)
        {
            var id = _materialRepository.AddMaterial(_mapper.Map<Material>(materialCreateDto));
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task DeleteMaterialAsync(int materialId)
        {
            await _materialRepository.DeleteMaterialAsync(materialId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<MaterialViewModel> FindMaterialByIdAsync(int materialId)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(materialId);
            var materialViewModel = _mapper.Map<MaterialViewModel>(material);
            return materialViewModel;
        }

        public async Task<IEnumerable<MaterialViewModel>> GetAllMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllAsync();
            var materialsViewModel = _mapper.Map<IEnumerable<MaterialViewModel>>(materials);
            return materialsViewModel;
        }

        public async Task UpdateMaterialAsync(int materialId, MaterialUpsertDto materialUpdateDto)
        {
            _materialRepository.UpdateMaterial(_mapper.Map<Material>(materialUpdateDto));
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
