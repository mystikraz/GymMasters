using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class CheckinService : ICheckinService
    {
        private readonly IRepository<Checkin> _repository;

        public CheckinService(IRepository<Checkin> repository)
        {
            _repository = repository;
        }
        public async Task<List<Checkin>> GetCheckins()
        {
            return  await _repository.GetAll()
                .Include(c => c.Member)
                    .ThenInclude(c => c.Memberships)
                .Where(x => x.CreatedAt.Date == DateTime.Today)
                .ToListAsync();
        }

        public async Task<Checkin> SaveAsync(Checkin checkin)
        {
            checkin.UpdateAt = DateTime.Now;
            checkin.CreatedAt = DateTime.Now;

            return await _repository.CreateAsync(checkin);
        }
        public async Task<bool> AlreadyCheckedIn(int memberId)
        {
            return await _repository.GetAll().AnyAsync(x => x.MemberId == memberId && x.CreatedAt.Date == DateTime.Today);
        }
        public async Task<Checkin?> GetById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool?> UpdateAsync(int id, Checkin checkin)
        {
            var exisingCheckin = await _repository.GetSingleAsync(id);
            if (exisingCheckin is null)
            {
                return null;
            }
            exisingCheckin.UpdateAt = DateTime.Now;
            exisingCheckin.MemberId = checkin.MemberId;
            exisingCheckin.Status = checkin.Status;
            exisingCheckin.EndDate = checkin.EndDate;
            return await _repository.UpdateAsync(exisingCheckin);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var exisingCheckin = await _repository.GetSingleAsync(id);
            if (exisingCheckin is null)
            {
                return null;
            }
            return await _repository.DeleteAsync(exisingCheckin);
        }

    }
}