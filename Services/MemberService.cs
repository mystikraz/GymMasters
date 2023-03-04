using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _repository;

        public MemberService(IRepository<Member> repository)
        {
            _repository = repository;
        }
        public async Task<List<Member>> GetMembers()
        {
            return await _repository.GetAll().Include(m => m.Trainer).ToListAsync();
        }
        public async Task<int> GetMembersCount()
        {
            return await _repository.GetAll().CountAsync();
        }
        public async Task<int> GetActiveMembers()
        {
            return await _repository.GetAll().Include(m => m.Memberships)
                                                .CountAsync(m => m.Memberships!.Any(ms => ms.EndDate > DateTime.Now));
        }
        public async Task<IEnumerable<Member>> GetExpiredMembers()
        {
            return await _repository.GetAll().Include(m => m.Memberships)
                                                .Where(m => m.Memberships!.Any(ms => ms.EndDate < DateTime.Now)).ToListAsync();
        }

        public async Task<int> GetInactiveMembers()
        {
            return await _repository.GetAll().Include(m => m.Checkins)
                                 .CountAsync(m => !m.Checkins.Any(c => c.CreatedAt >= DateTime.Today.AddMonths(-1)));
        }
        public async Task<Member?> GetMemberById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CheckIfExpired(int memberId)
        {
            return await _repository.GetAll().Include(x => x.Memberships).AnyAsync(x => x.Id == memberId && x.Memberships!.Any(x => x.EndDate < DateTime.Now));
        }
        public async Task<Member> SaveAsync(Member member)
        {
            member.UpdateAt = DateTime.Now;
            member.CreatedAt = DateTime.Now;

            return await _repository.CreateAsync(member);
        }

        public async Task<bool?> UpdateAsync(int id, Member member)
        {
            var exisingMember = await _repository.GetSingleAsync(id);
            if (exisingMember is null)
            {
                return null;
            }
            exisingMember.UpdateAt = DateTime.Now;
            exisingMember.Notes = member.Notes;
            exisingMember.Notes = member.Notes;
            exisingMember.Sex = member.Sex;
            exisingMember.Address = member.Address;
            exisingMember.TrainerId = member.TrainerId;

            return await _repository.UpdateAsync(exisingMember);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var exisingMember = await _repository.GetSingleAsync(id);
            if (exisingMember is null)
            {
                return null;
            }
            return await _repository.DeleteAsync(exisingMember);
        }

    }
}