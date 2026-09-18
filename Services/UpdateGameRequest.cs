using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record UpdateGameRequest([StringLength(100, MinimumLength = 1)] string? Name, [Range(0.01, 1000)] decimal? Price, [StringLength(50, MinimumLength = 1)] string? Genre);
}
