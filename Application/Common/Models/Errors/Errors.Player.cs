namespace Application.Common.Models.Errors
{
    public static partial class Errors
    {
        public static partial class Player
        {
            public static ClientError InvalidSkinFileError = new()
            {
                Error = "Player.InvalidSkinFileError",
                ErrorMessage = "Invalid skin file."
            };
        }
    }
}
