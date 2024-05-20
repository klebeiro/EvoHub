namespace EvoHub.Infra.CrossCutting
{
    public static class Constants
    {
        // Sucess messages
        public const string SucessRepositoriesRetrieved = "Repositories sucessfully retrieved.";
        public const string SucessFavoritesRetrieved = "Favorites sucessfully retrieved.";
        public const string SucessFavoriteSaved = "Favorite sucessfully saved.";
        public const string SucessFavoriteRemoved = "Favorite sucessfully removed.";

        // Error messages
        public const string ErrorRepositoryNotFound = "Repository not found";
        public const string ErrorCouldntRetrieveUserRepositories = "Couldn't retrieve user repositories.";
        public const string ErrorCouldntRetrieveRepositoriesRelatedToName = "Couldn't retrieve repositories related to the name.";
        public const string ErrorFavoriteAlreadyExists = "Favorite already Exists";
        public const string ErrorFavoriteFailedToSave = "Failed to save favorite.";
        public const string ErrorFavoriteFailedToRemove = "Failed to remove favorite.";
        public const string ErrorFavoriteDoesntExist = "Favorite doesn't exist";

        public static string GetErrorMessage(Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }

        // Other
        public const string DefaultOwner = "klebeiro";

    }
}
