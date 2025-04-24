using PractiseVilla.Models;

namespace PractiseVilla.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> Update(Villa villa);
    }
}
