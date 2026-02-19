using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopaSelectDormiApp.Entities.Clothe;

[Table("clothes")]
public class Clothe
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("name")] 
    [MaxLength(30)] 
    public string Name { get; set; } = null!;

    [Column("description")]
    [MaxLength(255)]
    public string? Description { get; set; } = null;
    
}