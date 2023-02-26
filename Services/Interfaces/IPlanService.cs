using Entities;

namespace Services.Interfaces
{
    public interface IPlanService
    {
        Task<bool?> DeleteAsync(int id);
        Task<Plan?> GetById(int id);
        Task<List<Plan>> GetPlans();
        Task<Plan> SaveAsync(Plan plan);
        Task<bool?> UpdateAsync(int id, Plan plan);
    }
}