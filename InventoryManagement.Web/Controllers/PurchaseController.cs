using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            ViewBag.Products = _context.Products.ToList();

            if (purchase.Quantity <= 0)
            {
                ModelState.AddModelError("", "Quantity must be greater than 0");
                return View(purchase);
            }

            var product = await _context.Products.FindAsync(purchase.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("", "Invalid product selected");
                return View(purchase);
            }

            // Increase Stock
            product.Quantity += purchase.Quantity;

            _context.Purchases.Add(purchase);

            _context.StockTransactions.Add(new StockTransaction
            {
                ProductId = purchase.ProductId,
                Quantity = purchase.Quantity,
                Type = "Purchase",
                TransactionDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product");
        }
    }
}