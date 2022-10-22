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
using Xunit;

namespace UnitTests.Services
{
    public class ColorServiceTests
    {
        private readonly Mock<IColorRepository> _colorRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly ColorService _colorService;

        public ColorServiceTests()
        {
            _colorRepositoryMock = new Mock<IColorRepository>();
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _colorService = new ColorService(_colorRepositoryMock.Object, _unitOfWorkStub.Object, _mapperStub.Object);
        }

        [Fact]
        public async Task GetAllColorsAsync_ReturnsAllColors()
        {
            // Arrange
            var colors = new List<Color>()
            {
                new Color() { Id = 1, Name = "Kék" },
                new Color() { Id = 2, Name = "Piros" }
            };

            var colorsViewModel = new ColorsViewModel();

            colorsViewModel.Colors = new List<ColorViewModel>()
            {
                new ColorViewModel() {  Id = 1, Name= "Kék"},
                new ColorViewModel() {  Id = 2, Name = "Piros" },
            };


            _colorRepositoryMock.Setup(repo => repo.GetAllAsync().Result)
                .Returns(colors);

            _mapperStub.Setup(mapper => mapper.Map<ColorsViewModel>(colors))
                .Returns(colorsViewModel);

            //Act
            var result = await _colorService.GetAllColorsAsync();
            var resultList = result.Colors.ToList();

            //Assert
            Assert.IsType<ColorsViewModel>(result);
            Assert.Equal(2, resultList.Count);
        }

        [Fact]
        public async Task FindColorByIdAsync_WithValidId_ReturnsExactColor()
        {
            // Arrange
            var color = new Color() { Id = 1, Name = "Kék" };

            _colorRepositoryMock.Setup(repo => repo.GetColorByIdAsync(1).Result)
                .Returns(color);

            var colorViewModel = new ColorViewModel() { Id = 1, Name = "Kék" };

            _mapperStub.Setup(mapper => mapper.Map<ColorViewModel>(color))
                .Returns(colorViewModel);

            //Act
            var result = await _colorService.FindColorByIdAsync(1);

            //Assert
            Assert.IsType<ColorViewModel>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Kék", result.Name);
        }

        [Fact]
        public async Task FindColorByIdAsync_WithInvalidId_ThrowsEntityNotFoundException()
        {
            //Arrange
            _colorRepositoryMock.Setup(repo => repo.GetColorByIdAsync(1).Result)
                .Throws(new EntityNotFoundException($"Color with id {1} not found."));

            //Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _colorService.FindColorByIdAsync(1));
        }

        [Fact]
        public async Task CreateColorAsync_WithValidName_CreatesExactColor()
        {
            // Arrange
            var colorDto = new ColorUpsertDto() { Name = "Kék" };
            var color = new Color() { Id = 1, Name = "Kék" };

            _colorRepositoryMock.Setup(repo => repo.AddColor(color))
                .Returns(1);

            _mapperStub.Setup(mapper => mapper.Map<Color>(colorDto))
                .Returns(color);

            //Act
            var result = await _colorService.CreateColorAsync(colorDto);

            //Assert
            Assert.IsType<EntityCreatedViewModel>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeleteColorAsync_WithValidId_CallsDeleteMethodInRepository()
        {
            // Arrange

            //Act
            await _colorService.DeleteColorAsync(1);

            //Assert
            _colorRepositoryMock.Verify(c => c.DeleteColorAsync(1), Times.Once);
        }
    }
}
