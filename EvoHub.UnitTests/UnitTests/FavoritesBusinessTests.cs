using EvoHub.Business.Contract;
using EvoHub.Business.Services;
using EvoHub.Domain;
using EvoHub.Infra.Contract;
using EvoHub.Infra.CrossCutting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class FavoritesBusinessTests
    {
        private readonly Mock<IFavoritesRepository> _mockFavoritesRepository;
        private readonly Mock<IGitHubApiBusiness> _mockGitHubApiBusiness;
        private readonly FavoritesBusiness _favoritesBusiness;

        public FavoritesBusinessTests()
        {
            _mockFavoritesRepository = new Mock<IFavoritesRepository>();
            _mockGitHubApiBusiness = new Mock<IGitHubApiBusiness>();
            _favoritesBusiness = new FavoritesBusiness(_mockFavoritesRepository.Object, _mockGitHubApiBusiness.Object);
        }

        [Fact]
        public async Task GetFavoriteRepositories_ShouldReturnListOfFavorites()
        {
            // Arrange
            var favorites = new List<Favorite>
        {
            new Favorite { Id = 1, Name = "Repo1", Owner = "Owner1", Description = "Description1", Language = "C#", UpdateLast = new DateTimeOffset(), Url = "http://repo1.com" },
            new Favorite { Id = 2, Name = "Repo2", Owner = "Owner2", Description = "Description2", Language = "Java", UpdateLast = new DateTimeOffset(), Url = "http://repo2.com" }
        };
            _mockFavoritesRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(favorites);

            // Act
            var result = await _favoritesBusiness.GetFavoriteRepositories();

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(2, result.Result.Count);
            Assert.Equal("Repo1", result.Result[0].Name);
            Assert.Equal("Repo2", result.Result[1].Name);
        }

        [Fact]
        public async Task SaveRepositoryInFavorites_ShouldSaveAndReturnFavorite()
        {
            // Arrange
            var repository = new GitHubRepositoryViewModel { Id = 1, Name = "Repo1", Owner = new Owner { Login = "Owner1" }, Description = "Description1", Language = "C#", UpdatedAt = new DateTimeOffset(), Url = new Uri("http://repo1.com") };
            _mockGitHubApiBusiness.Setup(api => api.GetOwnerRepository(It.IsAny<string>(), It.IsAny<long>())).ReturnsAsync(new ActionResult<GitHubRepositoryViewModel> { Result = repository });
            _mockFavoritesRepository.Setup(repo => repo.ExistsByCheckAlreadyAsync(It.IsAny<Favorite>())).ReturnsAsync(false);
            _mockFavoritesRepository.Setup(repo => repo.InsertAsync(It.IsAny<Favorite>())).ReturnsAsync(true);

            // Act
            var result = await _favoritesBusiness.SaveRepositoryInFavorites(1);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal("Repo1", result.Result.Name);
            Assert.Equal("Owner1", result.Result.Owner);
        }

        [Fact]
        public async Task SaveRepositoryInFavorites_ShouldReturnErrorWhenFavoriteAlreadyExists()
        {
            // Arrange
            var repository = new GitHubRepositoryViewModel { Id = 1, Name = "Repo1", Owner = new Owner { Login = "Owner1" }, Description = "Description1", Language = "C#", UpdatedAt = new DateTimeOffset(), Url = new Uri("http://repo1.com") };
            _mockGitHubApiBusiness.Setup(api => api.GetOwnerRepository(It.IsAny<string>(), It.IsAny<long>())).ReturnsAsync(new ActionResult<GitHubRepositoryViewModel> { Result = repository });
            _mockFavoritesRepository.Setup(repo => repo.ExistsByCheckAlreadyAsync(It.IsAny<Favorite>())).ReturnsAsync(true);

            // Act
            var result = await _favoritesBusiness.SaveRepositoryInFavorites(1);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(Constants.ErrorFavoriteAlreadyExists, result.Message);
        }

        [Fact]
        public async Task RemoveRepositoryFromFavorites_ShouldRemoveAndReturnFavorite()
        {
            // Arrange
            var repository = new GitHubRepositoryViewModel { Id = 1, Name = "Repo1", Owner = new Owner { Login = "Owner1" }, Description = "Description1", Language = "C#", UpdatedAt = new DateTimeOffset(), Url = new Uri("http://repo1.com") };
            _mockGitHubApiBusiness.Setup(api => api.GetOwnerRepository(It.IsAny<string>(), It.IsAny<long>())).ReturnsAsync(new ActionResult<GitHubRepositoryViewModel> { Result = repository });
            _mockFavoritesRepository.Setup(repo => repo.ExistsByCheckAlreadyAsync(It.IsAny<Favorite>())).ReturnsAsync(true);
            _mockFavoritesRepository.Setup(repo => repo.RemoveAsync(It.IsAny<Favorite>())).ReturnsAsync(true);

            // Act
            var result = await _favoritesBusiness.RemoveRepositoryFromFavorites(1);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal("Repo1", result.Result.Name);
            Assert.Equal("Owner1", result.Result.Owner);
        }

        [Fact]
        public async Task RemoveRepositoryFromFavorites_ShouldReturnErrorWhenFavoriteDoesNotExist()
        {
            // Arrange
            var repository = new GitHubRepositoryViewModel { Id = 1, Name = "Repo1", Owner = new Owner { Login = "Owner1" }, Description = "Description1", Language = "C#", UpdatedAt = new DateTimeOffset(), Url = new Uri("http://repo1.com") };
            _mockGitHubApiBusiness.Setup(api => api.GetOwnerRepository(It.IsAny<string>(), It.IsAny<long>())).ReturnsAsync(new ActionResult<GitHubRepositoryViewModel> { Result = repository });
            _mockFavoritesRepository.Setup(repo => repo.ExistsByCheckAlreadyAsync(It.IsAny<Favorite>())).ReturnsAsync(false);

            // Act
            var result = await _favoritesBusiness.RemoveRepositoryFromFavorites(1);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(Constants.ErrorFavoriteDoesntExist, result.Message);
        }
    }
}