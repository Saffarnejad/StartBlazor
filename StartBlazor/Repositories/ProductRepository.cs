using Microsoft.EntityFrameworkCore;
using StartBlazor.Data;
using StartBlazor.Repositories.Contracts;

namespace StartBlazor.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (product != null)
            {
                _context.Products.Remove(product);
                return (await _context.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                return product;
            }
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(product => product.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productInDb = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productInDb != null)
            {
                productInDb.Name = product.Name;
                productInDb.Price = product.Price;
                productInDb.Description = product.Description;
                //productInDb.SpecialTag = product.SpecialTag;
                productInDb.ImageUrl = product.ImageUrl;
                productInDb.CategoryId = product.CategoryId;
                _context.Products.Update(productInDb);
                await _context.SaveChangesAsync();
                return product;
            }
            return new Product();
        }
    }
}
