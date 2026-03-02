using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // List all products
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync(); // updated
            return View(products);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product); // updated
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id); // updated
            if (product == null)
                return NotFound();
            return View(product);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product); // updated
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Delete GET
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id); // updated
            if (product == null)
                return NotFound();
            return View(product);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id); // updated
            return RedirectToAction(nameof(Index));
        }
    }
}
