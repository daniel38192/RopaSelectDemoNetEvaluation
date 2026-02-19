using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopaSelectDormiApp.Entities.ClotheListElement;

using ClotheList;
using Clothe;

[Table("clothes_list_elements")]
public class ClotheListElement
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [ForeignKey(nameof(ClotheList.Id))]
    [Column("id_clothes_list")]
    public long ClotheListId { get; set; }
    
    [ForeignKey(nameof(Clothe.Id))]
    [Column("id_clothes")]
    public long ClotheId { get; set; }
    
    [Column("quantity")]
    public long Quantity { get; set; }
    
    public ClotheList ClotheList { get; set; } = null!;
    
    public Clothe Clothe { get; set; } = null!;
}