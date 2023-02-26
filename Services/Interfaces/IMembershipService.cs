using Entities;

namespace Services.Interfaces
{
    public interface IMembershipService
    {
        Task<bool?> DeleteAsync(int id);
        Task<Membership?> GetById(int id);
        Task<List<Membership>> GetMemberships();
        Task<Membership> SaveAsync(Membership membership);
        Task<bool?> UpdateAsync(int id, Membership membership);
    }
}