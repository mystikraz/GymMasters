using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IRepository<Membership> _repository;

        public MembershipService(IRepository<Membership> repository)
        {
            _repository = repository;
        }
        public async Task<List<Membership>> GetMemberships()
        {
            return await _repository.GetAll()
                .Include(m => m.Member)
                .Include(m => m.Plan).ToListAsync();
        }

        public async Task<Membership> SaveAsync(Membership membership)
        {
            membership.UpdateAt = DateTime.Now;
            membership.CreatedAt = DateTime.Now;

            return await _repository.CreateAsync(membership);
        }
        public async Task<Membership?> GetById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool?> UpdateAsync(int id, Membership membership)
        {
            var exisingMembership = await _repository.GetSingleAsync(id);
            if (exisingMembership is null)
            {
                return null;
            }
            exisingMembership.UpdateAt = DateTime.Now;
            exisingMembership.EndDate = membership.EndDate;
            exisingMembership.StartDate = membership.StartDate;
            exisingMembership.Price = membership.Price;
            exisingMembership.MemberId = membership.MemberId;

            return await _repository.UpdateAsync(exisingMembership);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var exisingMembership = await _repository.GetSingleAsync(id);
            if (exisingMembership is null)
            {
                return null;
            }
            return await _repository.DeleteAsync(exisingMembership);
        }

    }
}