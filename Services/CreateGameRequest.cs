namespace GameStore.Api.Services
{
    public record CreateGameRequest(string Name, decimal Price, string Genre);
}
