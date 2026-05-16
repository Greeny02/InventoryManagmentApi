namespace InventoryManagementAPI.Models
{
    public class ScanRequest
    {
        public string Barcode { get; set; } = string.Empty;
        public string? Name { get; set; }
    }
}
