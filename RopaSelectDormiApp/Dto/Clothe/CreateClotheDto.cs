using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RopaSelectDormiApp.Dto.Clothe;

public class CreateClotheDto
{
    [Required]
    [StringLength(30, MinimumLength = 4)]
    public required string Name { get; set; }
    public string? Description { get; set; }
}