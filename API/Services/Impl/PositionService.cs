using API.DbContext;
using API.Model;
using API.Requests;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Impl
{
    public class PositionService :IPositionService
    {
        public IDbContext DbContext { get; }

        public PositionService(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task DeleteAsync(int key)
        {
            var position = await DbContext
                    .Positions
                    .Include(x => x.Employees)
                    .FirstOrDefaultAsync(x => x.Id == key)
                ??
                    throw new Exception($"Position not found by key = {key}");

            if (position.Employees.Any())
            {
                throw new Exception($"Position with key = {key} has employes, deleting is not possible");
            }

            DbContext.Positions.Remove(position);
            await DbContext.SaveAsync();
        }

        public async Task<Position> GetAsync(int key)
        {
            var position = await DbContext
                    .Positions
                    .Include(x => x.Employees)
                    .FirstOrDefaultAsync(x => x.Id == key)
                ??
                    throw new Exception($"Position not found by key = {key}");

            return position;
        }

        public async Task SetAsync(PositionSetRequest request)
        {
            var position = await DbContext
                    .Positions
                    .Include(x => x.Employees)
                    .FirstOrDefaultAsync(x => x.Id == request.Id)
                ??
                    throw new Exception($"Position with id = '{request.Id} is not found'");

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                position.Name = request.Name;
            }

            if (request.Grade.HasValue)
            {
                position.Grade = request.Grade.Value;
            }

            await DbContext.SaveAsync();
        }

        public async Task CreateAsync(PositionCreateRequest request)
        {
            var position = await DbContext
                    .Positions
                    .Include(x => x.Employees)
                    .FirstOrDefaultAsync(x => x.Name == request.Name);

            if (position is not null)
            {
                throw new Exception($"Position  with name = '{request.Name} is already exist");
            }

            DbContext.Positions.Add(new Position
            {
                Name = request.Name!,
                Grade = request.Grade!.Value,
            });

            await DbContext.SaveAsync();
        }
    }
}
