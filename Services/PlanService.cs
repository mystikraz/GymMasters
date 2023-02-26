using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;

namespace Services
{
    public class PlanService : IPlanService
    {
        private readonly IRepository<Plan> _repository;

        public PlanService(IRepository<Plan> repository)
        {
            _repository = repository;
        }
        public async Task<List<Plan>> GetPlans()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Plan> SaveAsync(Plan plan)
        {
            plan.UpdateAt = DateTime.Now;
            plan.CreatedAt = DateTime.Now;

            return await _repository.CreateAsync(plan);
        }
        public async Task<Plan?> GetById(int id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool?> UpdateAsync(int id, Plan plan)
        {
            var exisingPlan = await _repository.GetSingleAsync(id);
            if (exisingPlan is null)
            {
                return null;
            }
            exisingPlan.UpdateAt = DateTime.Now;
            exisingPlan.Notes = plan.Notes;
            exisingPlan.Duration = plan.Duration;
            exisingPlan.Name = plan.Name;
            exisingPlan.Price = plan.Price;
            return await _repository.UpdateAsync(exisingPlan);
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            var exisingPlan = await _repository.GetSingleAsync(id);
            if (exisingPlan is null)
            {
                return null;
            }
            return await _repository.DeleteAsync(exisingPlan);
        }

    }
}