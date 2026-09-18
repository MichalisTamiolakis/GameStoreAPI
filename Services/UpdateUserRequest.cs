using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public record UpdateUserRequest([StringLength(100)] string? DisplayName, [EmailAddress] string? Email);
}
