using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Contracts.Persistance;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAsync();
    Task<T> GetByIdAsync(int Id);
}


