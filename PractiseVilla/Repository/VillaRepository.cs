using PractiseVilla.Data;
using PractiseVilla.Models;
using PractiseVilla.Repository.IRepository;

namespace PractiseVilla.Repository


{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> Update(Villa villa)
        {
            _db.Update(villa);
            await _db.SaveChangesAsync();
            return villa;
        }

        
    }
}
