
using System.ComponentModel.DataAnnotations;

public class RoleDto
{
    [Required(ErrorMessage = "Role Name cannot be empty"), MinLength(2), MaxLength(30)]
    public string Name { get; set; }
}