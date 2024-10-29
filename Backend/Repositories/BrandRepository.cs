using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {

    public class BrandRepository: IRepository<Brand> {

        private StoreContext _context;

        public BrandRepository (StoreContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> get () =>
            await _context.Brands.ToListAsync();

        public async Task<Brand?> getById (int id) =>
            await _context.Brands.FindAsync(id);

        public async Task add (Brand brand) =>
            await _context.Brands.AddAsync(brand);

        public void update (Brand brand) {
            _context.Brands.Attach(brand);
            _context.Brands.Entry(brand).State = EntityState.Modified;
        }

        public void delete (Brand brand) =>
            _context.Brands.Remove(brand);

        public async Task save () =>
            await _context.SaveChangesAsync();

        public IEnumerable<Brand> search (Func<Brand, bool> filter) =>
            _context.Brands.Where(filter).ToList();

    }

}
