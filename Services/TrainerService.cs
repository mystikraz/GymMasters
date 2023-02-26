using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> _repository;

        public TrainerService(IRepository<Trainer> repository)
        {
            _repository = repository;
        }
        public async Task<List<Trainer>> GetTrainers()
        {
            return await _repository.GetAll().ToListAsync();
        }
        public async Task<int> GetTrainersCount()
        {
            return await _repository.GetAll().CountAsync();
        }

        public async Task<Trainer> SaveAsync(Trainer trainer)
        {
            trainer.UpdateAt = DateTime.Now;
            trainer.CreatedAt = DateTime.Now;

            return await _repository.CreateAsync(trainer);
        }
        public async Task<Trainer?> GetById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool?> UpdateAsync(int id, Trainer trainer)
        {
            var exisingTrainer = await _repository.GetSingleAsync(id);
            if (exisingTrainer is null)
            {
                return null;
            }
            exisingTrainer.UpdateAt = DateTime.Now;
            exisingTrainer.Notes = trainer.Notes;
            exisingTrainer.Sex = trainer.Sex;
            exisingTrainer.Address = trainer.Address;
            exisingTrainer.DateOfBirth = trainer.DateOfBirth;
            exisingTrainer.CreatedBy = trainer.CreatedBy;
            exisingTrainer.FirstName = trainer.FirstName;
            exisingTrainer.lastName = trainer.lastName;
            exisingTrainer.Email = trainer.Email;
            exisingTrainer.Notes = trainer.Notes;
            exisingTrainer.Salary = trainer.Salary;

            return await _repository.UpdateAsync(exisingTrainer);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var exisingTrainer = await _repository.GetSingleAsync(id);
            if (exisingTrainer is null)
            {
                return null;
            }
            return await _repository.DeleteAsync(exisingTrainer);
        }

    }
}