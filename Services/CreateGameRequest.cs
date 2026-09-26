using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record CreateGameRequest([Required, StringLength(100, MinimumLength = 1)] string Name, [Required, Range(0, 1000), DecimalScale(2)] decimal? Price, [Required, StringLength(50, MinimumLength = 1)] string Genre);
}
