namespace InventoryManagement.Domain.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } = null!; // "Purchase" or "Sale"

        public DateTime TransactionDate { get; set; }  // ✅ Correct name

        public Product Product { get; set; } = null!;
    }
}