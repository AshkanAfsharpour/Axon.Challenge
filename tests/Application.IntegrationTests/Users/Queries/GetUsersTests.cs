using Axon.Application.Common.Extentions;
using Axon.Application.Common.Interfaces;
using Axon.Application.Common.Models;
using Axon.Application.Users.Commands.CreateUser;
using Axon.Application.Users.Queries.GetUsers;
using Axon.Domain.Entities;
using Axon.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Axon.Presistence;

namespace Axon.Application.IntegrationTests.Users.Queries
{
    using static Testing;
    [TestFixture]
    public class GetUsersTests : TestBase
    {

        [Test]
        public async Task ShouldReturnAllUsers()
        {
            // Arrange
            await RunAsUserAsync(nameof(UserRoleTypes.User));

            await SendRequestAsync(new CreateUserCommand
            {
                Name = "Ashkan Afsharpour",
                Username = "Ashkan77af",
                Password = "Super Secret",
                RoleTitle = UserRoleTypes.GetRoleNameById(UserRoleTypes.User)
            });

            await _iRepository.InsertAsync(new User
            {
                Name = "Ashkan Afsharpour",
                Username = "Ashkan77af",
                Password = "Super Secret",
                SessionId = Guid.NewGuid(),
                RefreshToken = GetService<IjwtServices>().GenerateRefreshToken(),
                RoleId = UserRoleTypes.User
            });
            await _iRepository.SaveChangesAsync();


            // Act
            ServiceResponse<PaginatedList<User>> response = await SendRequestAsync(new GetAllUsersQuery());


            // Assert
            response.Data.Should().NotBeNull(); 
            response.Data.Items.Should().HaveCount(3);
        }
    }
}
