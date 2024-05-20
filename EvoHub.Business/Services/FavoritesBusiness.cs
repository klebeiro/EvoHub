using EvoHub.Domain.Interfaces;
using EvoHub.Domain;
using EvoHub.Infra.Contract;
using EvoHub.Infra.CrossCutting;
using EvoHub.Business.Contract;

namespace EvoHub.Business.Services
{
    public class FavoritesBusiness : IFavoritesBusiness
    {
        private readonly IFavoritesRepository _context;
        private readonly IGitHubApiBusiness _gitHubApi;

        public FavoritesBusiness(IFavoritesRepository context, IGitHubApiBusiness gitHubApi)
        {
            _context = context;
            _gitHubApi = gitHubApi;
        }

        public async Task<ActionResult<List<FavoriteViewModel>>> GetFavoriteRepositories()
        {
            var result = new ActionResult<List<FavoriteViewModel>>();

            var favorites = await _context.GetAllAsync();
            result.Result = favorites.Select(f => new FavoriteViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Owner = f.Owner,
                Description = f.Description,
                Language = f.Language,
                UpdateLast = f.UpdateLast,
                Url = f.Url
            }).ToList();

            result.IsValid = true;
            result.Message = Constants.SucessFavoritesRetrieved;

            return result;
        }

        public async Task<ActionResult<FavoriteViewModel>> SaveRepositoryInFavorites(long id)
        {
            var result = new ActionResult<FavoriteViewModel>();

            var repository = _gitHubApi.GetOwnerRepository(Constants.DefaultOwner, id).Result.Result;

            var favorite = new Favorite
            {
                Id = repository.Id,
                Name = repository.Name,
                Owner = repository.Owner.Login,
                Description = repository.Description,
                Language = repository.Language,
                UpdateLast = repository.UpdatedAt,
                Url = repository.Url.ToString(),
            };

            if (await _context.ExistsByCheckAlreadyAsync(favorite))
            {
                result.IsValid = false;
                result.Message = Constants.ErrorFavoriteAlreadyExists;
            }
            else
            {
                var success = await _context.InsertAsync(favorite);
                result.IsValid = success;
                result.Message = success ? Constants.SucessFavoriteSaved : Constants.ErrorFavoriteFailedToSave;
                if (success)
                {
                    result.Result = new FavoriteViewModel
                    {
                        Id = favorite.Id,
                        Name = favorite.Name,
                        Owner = favorite.Owner,
                        Description = favorite.Description,
                        Language = favorite.Language,
                        UpdateLast = favorite.UpdateLast,
                        Url = favorite.Url,
                    };
                }
            }

            return result;
        }

        public async Task<ActionResult<FavoriteViewModel>> RemoveRepositoryFromFavorites(long id)
        {
            var result = new ActionResult<FavoriteViewModel>();

            var repository = _gitHubApi.GetOwnerRepository(Constants.DefaultOwner, id).Result.Result;

            var favorite = new Favorite
            {
                Id = repository.Id,
                Name = repository.Name,
                Owner = repository.Owner.Login,
                Description = repository.Description,
                Language = repository.Language,
                UpdateLast = repository.UpdatedAt,
                Url = repository.Url.ToString(),
            };

            if (!await _context.ExistsByCheckAlreadyAsync(favorite))
            {
                result.IsValid = false;
                result.Message = Constants.ErrorFavoriteDoesntExist;
            }
            else
            {
                var success = await _context.RemoveAsync(favorite);
                result.IsValid = success;
                result.Message = success ? Constants.SucessFavoriteRemoved : Constants.ErrorFavoriteFailedToRemove;
                if (success)
                {
                    if (success)
                    {
                        result.Result = new FavoriteViewModel
                        {
                            Id = favorite.Id,
                            Name = favorite.Name,
                            Owner = favorite.Owner,
                            Description = favorite.Description,
                            Language = favorite.Language,
                            UpdateLast = favorite.UpdateLast,
                            Url = favorite.Url,
                        };
                    }
                }
            }

            return result;
        }
    }
}
