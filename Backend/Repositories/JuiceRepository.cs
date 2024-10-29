using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {

    public class JuiceRepository: IRepository<Juice> {

        private StoreContext _context;

        public JuiceRepository (StoreContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Juice>> get () =>
            await _context.Juices.ToListAsync();

        public async Task<Juice?> getById (int id) =>
            await _context.Juices.FindAsync(id);

        public async Task add (Juice juice) =>
            await _context.Juices.AddAsync(juice);

        public void update (Juice juice) {
            _context.Juices.Attach(juice);
            _context.Juices.Entry(juice).State = EntityState.Modified;
        }

        public void delete (Juice juice) =>
            _context.Juices.Remove(juice);

        public async Task save () => 
            await _context.SaveChangesAsync();

        public IEnumerable<Juice> search (Func<Juice, bool> filter) =>
            _context.Juices.Where(filter).ToList();

    }

}
