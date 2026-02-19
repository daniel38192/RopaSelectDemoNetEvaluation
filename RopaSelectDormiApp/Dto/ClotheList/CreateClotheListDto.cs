using System.ComponentModel.DataAnnotations;

namespace RopaSelectDormiApp.Dto.ClotheList;

public record CreateClotheListDto
{
    [StringLength(30)]
    public string? Name {get; set;}
}