using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //public ICollection<StockTransaction>? StockTransactions { get; set; }
    }
}