using API.Model;
using API.Requests;

namespace API.Services
{
    public interface IPositionService
    {
        Task DeleteAsync(int key);
        Task<Position> GetAsync(int key);
        Task SetAsync(PositionSetRequest request);
        Task CreateAsync(PositionCreateRequest request);
    }
}
