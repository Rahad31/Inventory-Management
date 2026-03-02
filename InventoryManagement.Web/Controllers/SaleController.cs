using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.Web.Controllers  // ✅ namespace should NOT be public
{
    public class SaleController : Controller
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<StockTransaction> _stockTransactionRepository;

        public SaleController(
            IRepository<Sale> saleRepository,
            IRepository<Product> productRepository,
            IRepository<StockTransaction> stockTransactionRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _stockTransactionRepository = stockTransactionRepository;
        }

        // List all sales
        public async Task<IActionResult> Index()
        {
            var sales = await _saleRepository.GetAllAsync();
            return View(sales);
        }

        // Create sale GET
        public async Task<IActionResult> Create()
        {
            var products = await _productRepository.GetAllAsync();
            ViewBag.Products = products;
            return View();
        }

        // Create sale POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                // Decrease product quantity
                var product = await _productRepository.GetByIdAsync(sale.ProductId);
                product.Quantity -= sale.Quantity;
                await _productRepository.UpdateAsync(product);

                // Add sale
                await _saleRepository.AddAsync(sale);

                // Add stock transaction
                var stockTransaction = new StockTransaction
                {
                    ProductId = sale.ProductId,
                    Quantity = sale.Quantity,
                    TransactionDate = DateTime.Now,
                    Type = "Sale"
                };
                await _stockTransactionRepository.AddAsync(stockTransaction);

                return RedirectToAction(nameof(Index));
            }
            var products = await _productRepository.GetAllAsync();
            ViewBag.Products = products;
            return View(sale);
        }
    }
}