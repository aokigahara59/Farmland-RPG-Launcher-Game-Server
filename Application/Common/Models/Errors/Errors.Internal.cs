namespace Application.Common.Models.Errors
{
    public static partial class Errors
    {
        public static partial class Internal
        {
            public static ClientError OperationFailed = new()
            {
                Error = "Application.OperationFailed",
                ErrorMessage = "An error occurred while processing your request."
            };
        }
    }
}
