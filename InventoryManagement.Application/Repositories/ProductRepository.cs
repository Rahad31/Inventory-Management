using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.Application.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}