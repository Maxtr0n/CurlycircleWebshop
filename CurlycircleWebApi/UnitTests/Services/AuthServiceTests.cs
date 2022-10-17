using AutoMapper;
using BLL.Dtos;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly Mock<ICartRepository> _cartRepositoryStub;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerStub;
        private readonly Mock<IConfiguration> _configurationStub;
        private readonly List<ApplicationUser> _users;

        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _cartRepositoryStub = new Mock<ICartRepository>();
            _configurationStub = MockConfiguration();

            _users = new List<ApplicationUser>
            {
                new ApplicationUser() {
                    Id = 1,
                    UserName = "TestAdmin",
                    Email = "testadmin@gmail.com",
                },
                new ApplicationUser() {
                    Id = 2,
                    UserName = "TestUser",
                    Email = "testuser@gmail.com"
                }
            };
            _userManagerStub = MockUserManager(_users);

            _authService = new AuthService(_userManagerStub.Object, _unitOfWorkStub.Object, _mapperStub.Object,
                _configurationStub.Object, _cartRepositoryStub.Object);
        }



        [Fact]
        public async Task LoginAsync_WithValidDto_ReturnsCorrectUserViewModel()
        {
            //Arrange
            var loginDto = new LoginDto()
            {
                Email = "testadmin@gmail.com",
                Password = "abc123",
                CartId = 1
            };

            var userViewModel = new UserViewModel()
            {
                Id = 1,
                CartId = 1,
                Email = "testadmin@gmail.com"
            };

            var userCart = new Cart()
            {
                Id = 1,
                ApplicationUserId = 1,
                ApplicationUser = _users[0],
            };

            _cartRepositoryStub.Setup(c => c.GetUserCartAsync(1).Result)
                .Returns(userCart);
            _mapperStub.Setup(m => m.Map<UserViewModel>(_users[0]))
                .Returns(userViewModel);

            //Act
            var result = await _authService.LoginAsync(loginDto);

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("testadmin@gmail.com", result.Email);
            Assert.Equal(1, result.CartId);
            Assert.Equal(Role.Admin, result.Role);
        }


        // UserManager does not have an empty constructor, so I need to create the mocked version here
        private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            mgr.Setup(u => u.FindByEmailAsync("testadmin@gmail.com").Result)
               .Returns(ls[0]);
            mgr.Setup(u => u.FindByEmailAsync("testuser@gmail.com").Result)
               .Returns(ls[1]);
            mgr.Setup(u => u.FindByIdAsync("1").Result)
               .Returns(ls[0]);
            mgr.Setup(u => u.FindByIdAsync("2").Result)
               .Returns(ls[1]);
            mgr.Setup(u => u.GetRolesAsync(ls[0]).Result)
               .Returns(new List<string>() { "Admin" });
            mgr.Setup(u => u.GetRolesAsync(ls[1]).Result)
               .Returns(new List<string>() { "User" });
            mgr.Setup(x => x.CheckPasswordAsync(ls[0], "abc123").Result)
                .Returns(true);
            mgr.Setup(x => x.CheckPasswordAsync(ls[1], "abc123").Result)
                .Returns(true);

            return mgr;
        }

        private static Mock<IConfiguration> MockConfiguration()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c["Jwt:SigningKey"])
                .Returns("aspdssfphlASDF.elgsaSDeGY_ASFP-aSPLasF_ASFPSFslfffsasfaSFQQWR.ASlbbbvxgg.rwalv");
            configuration.Setup(c => c["Jwt:TokenValidityInMinutes"])
                .Returns("5");
            configuration.Setup(c => c["Jwt:Issuer"])
                .Returns("TestIssuer");
            configuration.Setup(c => c["Jwt:Audience"])
                .Returns("TestAudience");

            return configuration;
        }
    }
}
