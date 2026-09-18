using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record UpdateGameRequest([Required, StringLength(100, MinimumLength = 1)] string Name, [Required, Range(0.01, 1000)] decimal Price, [Required, StringLength(50, MinimumLength = 1)] string Genre);
}
