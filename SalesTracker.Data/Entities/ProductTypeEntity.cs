
using System.ComponentModel.DataAnnotations;

public class ProductTypeEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<ProductEntity> Items { get; set; } = new List<ProductEntity>();
}
