using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record UpdateUserRequest([StringLength(32)] string? DisplayName, [EmailAddress, StringLength(254)] string? Email);
}
