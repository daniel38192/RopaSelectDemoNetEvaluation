using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopaSelectDormiApp.Entities.Clothe;

using RopaSelectDormiApp.Entities.User;

[Table("clothes")]
public class Clothe
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [ForeignKey(nameof(User.Id))]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [Column("name")] 
    [MaxLength(30)] 
    public string Name { get; set; } = null!;

    [Column("description")]
    [MaxLength(255)]
    public string? Description { get; set; } = null;
    
    public User User { get; set; } = null!;
    
}