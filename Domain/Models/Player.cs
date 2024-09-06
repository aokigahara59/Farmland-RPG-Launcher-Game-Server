namespace Domain.Models
{
    public class Player
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string Uuid { get; set; } 
        public string AccessToken { get; set; } 
        public string ServerId { get; set; }
    }
}
