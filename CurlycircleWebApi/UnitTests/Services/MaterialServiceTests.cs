using AutoMapper;
using BLL.Dtos;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    public class MaterialServiceTests
    {
        private readonly Mock<IMaterialRepository> _materialRepositoryStub;
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly MaterialService _materialService;

        public MaterialServiceTests()
        {
            _materialRepositoryStub = new Mock<IMaterialRepository>();
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _materialService = new MaterialService(_materialRepositoryStub.Object, _unitOfWorkStub.Object, _mapperStub.Object);
        }

        [Fact]
        public async Task GetAllMaterialsAsync_ReturnsAllMaterials()
        {
            // Arrange
            var materials = new List<Material>()
            {
                new Material() { Id = 1, Name = "Szövet" },
                new Material() { Id = 2, Name = "Fém" }
            };

            var materialsViewModel = new MaterialsViewModel();

            materialsViewModel.Materials = new List<MaterialViewModel>()
            {
                new MaterialViewModel() {  Id = 1, Name= "Szövet"},
                new MaterialViewModel() {  Id = 2, Name = "Fém" },
            };


            _materialRepositoryStub.Setup(repo => repo.GetAllAsync().Result)
                .Returns(materials);

            _mapperStub.Setup(mapper => mapper.Map<MaterialsViewModel>(materials))
                .Returns(materialsViewModel);

            //Act
            var result = await _materialService.GetAllMaterialsAsync();
            var resultList = result.Materials.ToList();

            //Assert
            Assert.IsType<MaterialsViewModel>(result);
            Assert.Equal(2, resultList.Count);
        }

        [Fact]
        public async Task FindMaterialByIdAsync_WithValidId_ReturnsExactMaterial()
        {
            // Arrange
            var material = new Material() { Id = 1, Name = "Szövet" };

            _materialRepositoryStub.Setup(repo => repo.GetMaterialByIdAsync(1).Result)
                .Returns(material);

            var materialViewModel = new MaterialViewModel() { Id = 1, Name = "Szövet" };

            _mapperStub.Setup(mapper => mapper.Map<MaterialViewModel>(material))
                .Returns(materialViewModel);

            //Act
            var result = await _materialService.FindMaterialByIdAsync(1);

            //Assert
            Assert.IsType<MaterialViewModel>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Szövet", result.Name);
        }

        [Fact]
        public async Task FindMaterialByIdAsync_WithInvalidId_ThrowsEntityNotFoundException()
        {
            //Arrange
            _materialRepositoryStub.Setup(repo => repo.GetMaterialByIdAsync(1).Result)
                .Throws(new EntityNotFoundException($"Material with id {1} not found."));

            //Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _materialService.FindMaterialByIdAsync(1));
        }

        [Fact]
        public async Task CreateMaterialAsync_WithValidName_CreatesExactMaterial()
        {
            // Arrange
            var materialDto = new MaterialUpsertDto() { Name = "Szövet" };
            var material = new Material() { Id = 1, Name = "Szövet" };

            _materialRepositoryStub.Setup(repo => repo.AddMaterial(material))
                .Returns(1);

            _mapperStub.Setup(mapper => mapper.Map<Material>(materialDto))
                .Returns(material);

            //Act
            var result = await _materialService.CreateMaterialAsync(materialDto);

            //Assert
            Assert.IsType<EntityCreatedViewModel>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeleteMaterialAsync_WithValidId_CallsDeleteMethodInRepository()
        {
            // Arrange

            //Act
            await _materialService.DeleteMaterialAsync(1);

            //Assert
            _materialRepositoryStub.Verify(c => c.DeleteMaterialAsync(1), Times.Once);
        }
    }
}
