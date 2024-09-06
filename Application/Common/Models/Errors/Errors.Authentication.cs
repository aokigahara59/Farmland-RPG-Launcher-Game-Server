namespace Application.Common.Models.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static ClientError DuplicateUsername = new()
            {
                Error = "Authentication.DuplicateUsername",
                ErrorMessage = "Username already exist."
            };

            public static ClientError InvalidCredentials = new()
            {
                Error = "Invalid uuid or access token",
                ErrorMessage = "Invalid uuid or access token. Try to relaunch the game.",
            };
        }
    }
}
