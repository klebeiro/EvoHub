using EvoHub.Business.Services;
using EvoHub.Domain;
using EvoHub.Domain.Interfaces;
using EvoHub.Infra.CrossCutting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class GitHubApiBusinessTests
    {
        private readonly Mock<IGitHubApi> _gitHubApiMock;
        private readonly GitHubApiBusiness _gitHubApiBusiness;

        public GitHubApiBusinessTests()
        {
            _gitHubApiMock = new Mock<IGitHubApi>();
            _gitHubApiBusiness = new GitHubApiBusiness(_gitHubApiMock.Object);
        }

        [Fact]
        public async Task GetDefaultOwnerRepositories_ValidOwner_ReturnsRepositories()
        {
            // Arrange
            var repositories = new List<GitHubRepository>
            {
                new GitHubRepository { Id = 1, Name = "Repo1" },
                new GitHubRepository { Id = 2, Name = "Repo2" }
            };
            var expectedResult = repositories.Select(repo => new GitHubRepositoryViewModel
            {
                Id = repo.Id,
                Name = repo.Name
            }).ToList();
            _gitHubApiMock.Setup(api => api.GetRepository(Constants.DefaultOwner))
                          .ReturnsAsync(new ActionResult<List<GitHubRepository>> { IsValid = true, Result = repositories });

            // Act
            var result = await _gitHubApiBusiness.GetDefaultOwnerRepositories();

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(expectedResult[0].Id, result.Result[0].Id);
        }

        [Fact]
        public async Task GetDefaultOwnerRepositories_InvalidOwner_ReturnsError()
        {
            // Arrange
            const string errorMessage = "Invalid owner";
            _gitHubApiMock.Setup(api => api.GetRepository(Constants.DefaultOwner))
                          .ReturnsAsync(new ActionResult<List<GitHubRepository>> { IsValid = false, Message = errorMessage });

            // Act
            var result = await _gitHubApiBusiness.GetDefaultOwnerRepositories();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Message);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetOwnerRepository_ExistingRepository_ReturnsRepository()
        {
            // Arrange
            const string owner = "Owner";
            const long repoId = 1;
            var repositories = new List<GitHubRepository>
            {
                new GitHubRepository { Id = 1, Name = "Repo1" },
                new GitHubRepository { Id = 2, Name = "Repo2" }
            };
            var expectedResult = new GitHubRepositoryViewModel { Id = 1, Name = "Repo1" };
            _gitHubApiMock.Setup(api => api.GetRepository(owner))
                          .ReturnsAsync(new ActionResult<List<GitHubRepository>> { IsValid = true, Result = repositories });

            // Act
            var result = await _gitHubApiBusiness.GetOwnerRepository(owner, repoId);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(expectedResult.Id, result.Result.Id);
        }

        [Fact]
        public async Task GetOwnerRepository_NonexistentRepository_ReturnsError()
        {
            // Arrange
            const string owner = "Owner";
            const long repoId = 3;
            const string errorMessage = "Repository not found";
            var repositories = new List<GitHubRepository>
            {
                new GitHubRepository { Id = 1, Name = "Repo1" },
                new GitHubRepository { Id = 2, Name = "Repo2" }
            };
            _gitHubApiMock.Setup(api => api.GetRepository(owner))
                          .ReturnsAsync(new ActionResult<List<GitHubRepository>> { IsValid = true, Result = repositories });

            // Act
            var result = await _gitHubApiBusiness.GetOwnerRepository(owner, repoId);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Message);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetOwnerRepository_InvalidOwner_ReturnsError()
        {
            // Arrange
            const string owner = "InvalidOwner";
            const long repoId = 1;
            const string errorMessage = "Invalid owner";
            _gitHubApiMock.Setup(api => api.GetRepository(owner))
                          .ReturnsAsync(new ActionResult<List<GitHubRepository>> { IsValid = false, Message = errorMessage });

            // Act
            var result = await _gitHubApiBusiness.GetOwnerRepository(owner, repoId);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Message);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetRepositoriesByName_ValidName_ReturnsRepositories()
        {
            // Arrange
            const string name = "Repo";
            var repositoriesResult = new RepositoryModel
            {
                Repositories = new List<GitHubRepository>
                {
                    new GitHubRepository() { Id = 1, Name = "Repo1" },
                    new GitHubRepository() { Id = 2, Name = "Repo2" }
                }.ToArray()
            };
            var expectedResult = repositoriesResult.Repositories.Select(repo => new GitHubRepositoryViewModel
            {
                Id = repo.Id,
                Name = repo.Name
            }).ToList();
            _gitHubApiMock.Setup(api => api.GetRepositoriesByName(name))
                          .ReturnsAsync(new ActionResult<RepositoryModel> { IsValid = true, Result = repositoriesResult });

            // Act
            var result = await _gitHubApiBusiness.GetRepositoriesByName(name);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(expectedResult[0].Id, result.Result[0].Id);
        }

        [Fact]
        public async Task GetRepositoriesByName_InvalidName_ReturnsError()
        {
            // Arrange
            const string name = "InvalidName";
            const string errorMessage = "Invalid name";
            _gitHubApiMock.Setup(api => api.GetRepositoriesByName(name))
                          .ReturnsAsync(new ActionResult<RepositoryModel> { IsValid = false, Message = errorMessage });

            // Act
            var result = await _gitHubApiBusiness.GetRepositoriesByName(name);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Message);
            Assert.Null(result.Result);
        }
    }
}