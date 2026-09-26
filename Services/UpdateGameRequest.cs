using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record UpdateGameRequest([StringLength(100, MinimumLength = 1)] string? Name, [Range(0, 1000), DecimalScale(2)] decimal? Price, [StringLength(50, MinimumLength = 1)] string? Genre);
}
