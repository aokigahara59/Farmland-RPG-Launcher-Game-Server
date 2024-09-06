namespace Contracts.Game
{
    public record JoinRequest(string AccessToken, string SelectedProfile, string ServerId);
}
