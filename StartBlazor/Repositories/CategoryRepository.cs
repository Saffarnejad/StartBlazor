using Microsoft.EntityFrameworkCore;
using StartBlazor.Data;
using StartBlazor.Repositories.Contracts;

namespace StartBlazor.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return (await _context.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                return category;
            }
            return new Category();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryInDb = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryInDb != null)
            {
                categoryInDb.Name = category.Name;
                _context.Categories.Update(categoryInDb);
                await _context.SaveChangesAsync();
                return category;
            }
            return new Category();
        }
    }
}
