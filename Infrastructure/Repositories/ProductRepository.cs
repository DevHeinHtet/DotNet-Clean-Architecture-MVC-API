using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await GetByIdAsync(product.ID);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with id {product.ID} not found.");
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
        }

        public async Task DeleteAsync(int id)
        {
            var existingProduct = await GetByIdAsync(id);
            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            return await _context.Products.AnyAsync(x => x.Name == name);
        }
    }
}