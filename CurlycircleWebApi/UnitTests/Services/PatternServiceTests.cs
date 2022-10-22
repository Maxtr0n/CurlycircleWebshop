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
    public class PatternServiceTests
    {
        private readonly Mock<IPatternRepository> _patternRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly PatternService _patternService;

        public PatternServiceTests()
        {
            _patternRepositoryMock = new Mock<IPatternRepository>();
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _patternService = new PatternService(_patternRepositoryMock.Object, _unitOfWorkStub.Object, _mapperStub.Object);
        }

        [Fact]
        public async Task GetAllPatternsAsync_ReturnsAllPatterns()
        {
            // Arrange
            var patterns = new List<Pattern>()
            {
                new Pattern() { Id = 1, Name = "Csíkos" },
                new Pattern() { Id = 2, Name = "Pöttyös" }
            };

            var patternsViewModel = new PatternsViewModel();

            patternsViewModel.Patterns = new List<PatternViewModel>()
            {
                new PatternViewModel() {  Id = 1, Name= "Csíkos"},
                new PatternViewModel() {  Id = 2, Name = "Pöttyös" },
            };


            _patternRepositoryMock.Setup(repo => repo.GetAllAsync().Result)
                .Returns(patterns);

            _mapperStub.Setup(mapper => mapper.Map<PatternsViewModel>(patterns))
                .Returns(patternsViewModel);

            //Act
            var result = await _patternService.GetAllPatternsAsync();
            var resultList = result.Patterns.ToList();

            //Assert
            Assert.IsType<PatternsViewModel>(result);
            Assert.Equal(2, resultList.Count);
        }

        [Fact]
        public async Task FindPatternByIdAsync_WithValidId_ReturnsExactPattern()
        {
            // Arrange
            var pattern = new Pattern() { Id = 1, Name = "Csíkos" };

            _patternRepositoryMock.Setup(repo => repo.GetPatternByIdAsync(1).Result)
                .Returns(pattern);

            var patternViewModel = new PatternViewModel() { Id = 1, Name = "Csíkos" };

            _mapperStub.Setup(mapper => mapper.Map<PatternViewModel>(pattern))
                .Returns(patternViewModel);

            //Act
            var result = await _patternService.FindPatternByIdAsync(1);

            //Assert
            Assert.IsType<PatternViewModel>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Csíkos", result.Name);
        }

        [Fact]
        public async Task FindPatternByIdAsync_WithInvalidId_ThrowsEntityNotFoundException()
        {
            //Arrange
            _patternRepositoryMock.Setup(repo => repo.GetPatternByIdAsync(1).Result)
                .Throws(new EntityNotFoundException($"Pattern with id {1} not found."));

            //Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _patternService.FindPatternByIdAsync(1));
        }

        [Fact]
        public async Task CreatePatternAsync_WithValidName_CreatesExactPattern()
        {
            // Arrange
            var patternDto = new PatternUpsertDto() { Name = "Csíkos" };
            var pattern = new Pattern() { Id = 1, Name = "Csíkos" };

            _patternRepositoryMock.Setup(repo => repo.AddPattern(pattern))
                .Returns(1);

            _mapperStub.Setup(mapper => mapper.Map<Pattern>(patternDto))
                .Returns(pattern);

            //Act
            var result = await _patternService.CreatePatternAsync(patternDto);

            //Assert
            Assert.IsType<EntityCreatedViewModel>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeletePatternAsync_WithValidId_CallsDeleteMethodInRepository()
        {
            // Arrange

            //Act
            await _patternService.DeletePatternAsync(1);

            //Assert
            _patternRepositoryMock.Verify(c => c.DeletePatternAsync(1), Times.Once);
        }
    }
}
