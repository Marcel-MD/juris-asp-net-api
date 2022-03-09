using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.User;

public class CreateUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}