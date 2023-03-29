using API.Model;
using API.Requests;

namespace API.Services
{
    public interface IEmployeeService
    {
        Task DeleteAsync(int key);
        Task<Employee> GetAsync(int key);
        Task SetAsync(EmployeeSetRequest request);
        Task CreateAsync(EmployeeCreateRequest request);
    }
}
