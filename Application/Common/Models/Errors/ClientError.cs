namespace Application.Common.Models.Errors
{
    public class ClientError
    {
        public string Error { get; set; }
        public string ErrorMessage { get; set; }
        public string Cause { get; set; }
    }
}
