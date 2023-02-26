using Entities;

namespace Services.Interfaces
{
    public interface IMemberService
    {
        Task<bool?> DeleteAsync(int id);
        Task<List<Member>> GetMembers();
        Task<Member> SaveAsync(Member member);
        Task<bool?> UpdateAsync(int id, Member member);
        Task<Member?> GetMemberById(int id);
        Task<bool> CheckIfExpired(int memberId);
        Task<int> GetMembersCount();
        Task<int> GetActiveMembers();
        Task<int> GetExpiredMembers();
        Task<int> GetInactiveMembers();
    }
}