using AutoMapper;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
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
        private readonly Mock<IColorRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ColorService _colorService;

        public ColorServiceTests()
        {
            _mockRepo = new Mock<IColorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _colorService = new ColorService(_mockRepo.Object, _mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllColorsAsync_HappyPath_ReturnsAllColors()
        {
            // Arrange
            var colors = new List<Color>()
            {
                new Color() { Id = 1, Name = "Kék" },
                new Color() { Id = 2, Name = "Piros" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync().Result)
                .Returns(colors);

            var colorsViewModel = new ColorsViewModel();
            colorsViewModel.Colors = new List<ColorViewModel>()
            {
                new ColorViewModel() {  Id = 1, Name= "Kék"},
                new ColorViewModel() {  Id = 2, Name = "Piros" },
            };

            _mockMapper.Setup(mapper => mapper.Map<ColorsViewModel>(colors))
                .Returns(colorsViewModel);

            //Act
            var result = await _colorService.GetAllColorsAsync();
            var resultList = result.Colors.ToList();

            //Assert
            Assert.IsType<ColorsViewModel>(result);
            Assert.Equal(2, resultList.Count);
        }
    }
}
