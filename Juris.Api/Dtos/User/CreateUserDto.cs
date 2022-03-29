using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.User;

public class CreateUserDto
{
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(250)]
    public string Password { get; set; }
}