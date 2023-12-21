using Domain.Entites;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
     
    }
}
