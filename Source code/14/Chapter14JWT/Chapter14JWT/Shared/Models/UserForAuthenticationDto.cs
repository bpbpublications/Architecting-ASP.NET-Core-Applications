using System.ComponentModel.DataAnnotations;

namespace Chapter14JWT.Shared.Models;

public class UserForAuthenticationDto
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}
