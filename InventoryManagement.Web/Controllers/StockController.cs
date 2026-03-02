using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using System.Linq;

namespace InventoryManagement.Web.Controllers
{
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Transaction History
        public IActionResult Index()
        {
            // Order transactions by TransactionDate (not Date)
            var transactions = _context.StockTransactions
                .OrderByDescending(x => x.TransactionDate)
                .ToList();

            return View(transactions);
        }

        // Ajax Stock Check
        public JsonResult GetStock(int id)
        {
            var stock = _context.Products.Find(id)?.Quantity ?? 0;
            return Json(stock);
        }

        // Optional: Current Stock of all products
        public IActionResult CurrentStock()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Optional: Product-wise transaction history
        public IActionResult ProductTransactions(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound();

            var transactions = _context.StockTransactions
                .Where(t => t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();

            ViewBag.Product = product;
            return View(transactions);
        }
    }
}