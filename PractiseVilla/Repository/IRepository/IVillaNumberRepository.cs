using PractiseVilla.Models;

namespace PractiseVilla.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {

        Task<VillaNumber> Update(VillaNumber villaNumber);
    }
}
