using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementAPI.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id", TypeName = "varchar(36)")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("barcode", TypeName = "varchar(50)")]
        public string Barcode { get; set; } = string.Empty;

        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; } = string.Empty;

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
