using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record CreateUserRequest([Required, StringLength(100, MinimumLength = 1)] string Name, [Required, StringLength(100, MinimumLength = 1)] string Surname, [StringLength(32)] string? DisplayName, [Required, EmailAddress, StringLength(254)] string Email);
}
