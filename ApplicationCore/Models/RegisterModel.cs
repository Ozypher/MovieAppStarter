using System.ComponentModel.DataAnnotations;
using ApplicationCore.Validators;

namespace ApplicationCore.Models;

public class RegisterModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Email not formatted correctly.")]
    [StringLength(50,ErrorMessage = "Email cannot exceed 50 characters")]
    public string Email { get; set; }
    [Required]
    [StringLength(50,ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"
        ,ErrorMessage = "Must contain 8 letters one uppercase one lowercase and a special character.")]
    public string Password { get; set; }
    
    // year should not be less than 1900
    // Minimum age should be 15
    
    [MinimumAllowedYear]
    public DateTime DateOfBirth { get; set; }
}