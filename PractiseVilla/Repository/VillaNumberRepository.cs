using PractiseVilla.Data;
using PractiseVilla.Models;
using PractiseVilla.Repository.IRepository;

namespace PractiseVilla.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public Task<VillaNumber> Update(VillaNumber villaNumber)
        {
            throw new NotImplementedException();
        }
    }
}
