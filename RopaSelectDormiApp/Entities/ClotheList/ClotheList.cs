using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopaSelectDormiApp.Entities.ClotheList;

[Table("clothes_list")]
public class ClotheList
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    [Column("name")]
    public string Name { get; set; } = null!;
    
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    List<ClotheListElement.ClotheListElement> ClotheListElements { get; set; } = [];
}