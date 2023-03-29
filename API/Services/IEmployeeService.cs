using API.Requests;
using API.Responses;

namespace API.Services
{
    public interface IEmployeeService
    {
        Task DeleteAsync(int key);
        Task<EmployeeResponse> GetAsync(int key);
        Task SetAsync(EmployeeSetRequest request);
        Task<int> CreateAsync(EmployeeCreateRequest request);
    }
}
