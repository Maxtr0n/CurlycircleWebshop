using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
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
                    Address = new UserAddress()
                },
                new ApplicationUser() {
                    Id = 2,
                    UserName = "TestUser",
                    Email = "testuser@gmail.com",
                    Address = new UserAddress()
                }
            };
            _userManagerStub = MockUserManager(_users);

            _authService = new AuthService(_userManagerStub.Object, _unitOfWorkStub.Object, _mapperStub.Object,
                _configurationStub.Object, _cartRepositoryStub.Object);
        }

        [Fact]
        public async Task LoginAsync_AdminWithCorrectPassword_ReturnsUserViewModel()
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

        [Fact]
        public async Task LoginAsync_AdminWithWrongPassword_ReturnsUserViewModel()
        {
            //Arrange
            var loginDto = new LoginDto()
            {
                Email = "testadmin@gmail.com",
                Password = "abc124",
                CartId = 1
            };

            //Act & Assert
            await Assert.ThrowsAsync<ValidationAppException>(() => _authService.LoginAsync(loginDto));
        }

        [Fact]
        public async Task LoginAsync_UserWithCorrectPassword_ReturnsUserViewModel()
        {
            //Arrange
            var loginDto = new LoginDto()
            {
                Email = "testuser@gmail.com",
                Password = "abc123",
                CartId = 2
            };

            var userViewModel = new UserViewModel()
            {
                Id = 2,
                CartId = 2,
                Email = "testuser@gmail.com"
            };

            var userCart = new Cart()
            {
                Id = 2,
                ApplicationUserId = 2,
                ApplicationUser = _users[1],
            };

            _cartRepositoryStub.Setup(c => c.GetUserCartAsync(2).Result)
                .Returns(userCart);
            _mapperStub.Setup(m => m.Map<UserViewModel>(_users[1]))
                .Returns(userViewModel);

            //Act
            var result = await _authService.LoginAsync(loginDto);

            //Assert
            Assert.Equal(2, result.Id);
            Assert.Equal("testuser@gmail.com", result.Email);
            Assert.Equal(2, result.CartId);
            Assert.Equal(Role.User, result.Role);
        }

        [Fact]
        public async Task LoginAsync_UserWithWrongPassword_ReturnsUserViewModel()
        {
            //Arrange
            var loginDto = new LoginDto()
            {
                Email = "testuser@gmail.com",
                Password = "abc124",
                CartId = 2
            };

            //Act & Assert
            await Assert.ThrowsAsync<ValidationAppException>(() => _authService.LoginAsync(loginDto));
        }

        [Fact]
        public async Task LoginAsync_UserWithNewCart_MergesCartsAndReturnsUserViewModel()
        {
            //Arrange
            var loginDto = new LoginDto()
            {
                Email = "testuser@gmail.com",
                Password = "abc123",
                CartId = 3
            };

            var userViewModel = new UserViewModel()
            {
                Id = 2,
                CartId = 2,
                Email = "testuser@gmail.com"
            };

            var userCart = new Cart()
            {
                Id = 2,
                ApplicationUserId = 2,
                ApplicationUser = _users[1],
            };

            userCart.CartItems.Add(new CartItem()
            {
                Id = 2,
                Price = 2000
            }); ;

            var cartBeforeLogin = new Cart()
            {
                Id = 3
            };

            cartBeforeLogin.CartItems.Add(new CartItem()
            {
                Id = 1,
                Price = 1000
            });

            _cartRepositoryStub.Setup(c => c.GetUserCartAsync(2).Result)
                .Returns(userCart);
            _cartRepositoryStub.Setup(c => c.GetCartByIdAsync(3).Result)
                .Returns(cartBeforeLogin);
            _cartRepositoryStub.Setup(c => c.AddCartItemAsync(2, cartBeforeLogin.CartItems[0])).Callback(() =>
            {
                userCart.CartItems.Add(cartBeforeLogin.CartItems[0]);
            });
            _mapperStub.Setup(m => m.Map<UserViewModel>(_users[1]))
                .Returns(userViewModel);

            //Act
            var result = await _authService.LoginAsync(loginDto);

            //Assert
            Assert.Equal(2, result.Id);
            Assert.Equal("testuser@gmail.com", result.Email);
            Assert.Equal(2, result.CartId);
            Assert.Equal(Role.User, result.Role);
            Assert.Equal(2, userCart.CartItems.Count);
        }

        [Fact]
        public async Task RegisterAsync_ValidDto_ReturnsEntityCreatedViewModel()
        {
            //Arrange
            var registerDto = new RegisterDto()
            {
                Email = "testuser2@gmail.com",

            };

            var user = new ApplicationUser()
            {
                Id = 3,
                Email = "testuser2@gmail.com",
            };

            _mapperStub.Setup(m => m.Map<ApplicationUser>(registerDto))
                .Returns(user);
            //Act
            var result = await _authService.RegisterAsync(registerDto);

            //Assert
            Assert.Equal(3, result.Id);
            Assert.Equal(3, _users.Count);
        }

        [Fact]
        public async Task RegisterAsync_EmailTaken_ThrowsValidationAppException()
        {
            //Arrange
            var registerDto = new RegisterDto()
            {
                Email = "testuser@gmail.com",

            };

            //Act & Assert
            await Assert.ThrowsAsync<ValidationAppException>(() => _authService.RegisterAsync(registerDto));
        }

        [Fact]
        public async Task UpdateUserAsync_ValidDto_UpdatesUserData()
        {
            //Arrange
            var updateDto = new UserUpdateDto()
            {
                UserId = 1,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                PhoneNumber = "1",
                City = "NewCity"
            };

            var userDataViewModel = new UserDataViewModel()
            {
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                PhoneNumber = "1",
                City = "NewCity"
            };

            _mapperStub.Setup(m => m.Map<UserDataViewModel>(_users[0]))
                .Returns(userDataViewModel);

            //Act
            var result = await _authService.UpdateUserAsync(updateDto);

            //Assert
            Assert.Equal("NewFirstName", result.FirstName);
            Assert.Equal("NewLastName", result.LastName);
            Assert.Equal("1", result.PhoneNumber);
            Assert.Equal("NewCity", result.City);
        }

        [Fact]
        public async Task ChangePasswordAsync_WithCorrectPassword_ChangesPassword()
        {
            //Arrange
            var changePasswordDto = new ChangePasswordDto()
            {
                Email = "testuser@gmail.com",
                OldPassword = "abc123",
                NewPassword = "abc234"
            };

            //Act
            await _authService.ChangePasswordAsync(changePasswordDto);

            //Assert
            _userManagerStub.Verify(u => u.CheckPasswordAsync(_users[1], changePasswordDto.OldPassword), Times.Once);
            _userManagerStub.Verify(u => u.ChangePasswordAsync(_users[1], changePasswordDto.OldPassword, changePasswordDto.NewPassword), Times.Once);
        }

        [Fact]
        public async Task ChangePasswordAsync_WithInvalidPassword_ThrowsValidationAppException()
        {
            //Arrange
            var changePasswordDto = new ChangePasswordDto()
            {
                Email = "testuser@gmail.com",
                OldPassword = "abc121",
                NewPassword = "abc234"
            };

            //Act & Assert
            await Assert.ThrowsAsync<ValidationAppException>(() => _authService.ChangePasswordAsync(changePasswordDto));
        }

        [Fact]
        public async Task DeleteUserAsync_WithValidPassword_DeletesCorrectUser()
        {
            var deleteUsertDto = new DeleteUserDto()
            {
                Id = 2,
                Password = "abc123"
            };

            await _authService.DeleteUserAsync(deleteUsertDto);

            _userManagerStub.Verify(u => u.DeleteAsync(_users[1]), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_WithWrongPassword_DeletesCorrectUser()
        {
            var deleteUsertDto = new DeleteUserDto()
            {
                Id = 2,
                Password = "abd123"
            };

            await Assert.ThrowsAsync<ValidationAppException>(() => _authService.DeleteUserAsync(deleteUsertDto));
        }

        [Fact]
        public async Task GetUserDataAsync_WithValidUserId_ReturnsUserData()
        {
            var userDataViewModel = new UserDataViewModel()
            {
                Email = "testuser@gmail.com"
            };

            _mapperStub.Setup(m => m.Map<UserDataViewModel>(_users[1]))
                .Returns(userDataViewModel);

            var result = await _authService.GetUserDataAsync(2);

            Assert.Equal("testuser@gmail.com", result.Email);
        }

        [Fact]
        public async Task GetUserDataAsync_WithWrongUserId_ReturnsUserData()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _authService.GetUserDataAsync(3));
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
            mgr.Setup(x => x.ChangePasswordAsync(It.IsAny<TUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

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
