using API.DbContext;
using API.Model;
using API.Requests;
using API.Responses;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        public IDbContext DbContext { get; }

        public EmployeeService(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task DeleteAsync(int key)
        {
            var employee = await DbContext
                    .Employees
                    .FirstOrDefaultAsync(x => x.Id == key) 
                ?? 
                    throw new Exception($"Employee not found by key = {key}");

            DbContext.Employees.Remove(employee);
            await DbContext.SaveAsync();
        }

        public async Task<EmployeeResponse> GetAsync(int key)
        {
            var employee = await DbContext
                    .Employees
                    .Include(x => x.Positions)
                    .FirstOrDefaultAsync(x => x.Id == key)
                ?? 
                    throw new Exception($"Employee not found by key = {key}");

            return new EmployeeResponse
            {
                FIO = employee.FIO,
                Birthday = employee.Birthday,
                Positions = employee.Positions.Select(x => new PositionResponse
                {
                    Grage = x.Grade,
                    Name = x.Name,
                }).ToList(),
            };
        }

        public async Task SetAsync(EmployeeSetRequest request)
        {
            var employee = await DbContext
                    .Employees
                    .Include(x => x.Positions)
                    .FirstOrDefaultAsync(x => x.Id == request.Id)
                ??
                    throw new Exception($"Employee with id = {request.Id} is not found");

            if (!string.IsNullOrWhiteSpace(request.FIO))
            {
                employee.FIO = request.FIO;
            }

            if (request.Birthday.HasValue)
            {
                employee.Birthday = request.Birthday.Value;
            }

            if (request.Positions is not null)
            {
                FillPositionsByIds(employee, request.Positions);
            }

            await DbContext.SaveAsync();
        }

        public async Task<int> CreateAsync(EmployeeCreateRequest request)
        {
            var employee = await DbContext
                    .Employees
                    .Include(x => x.Positions)
                    .FirstOrDefaultAsync(x => x.FIO == request.FIO && x.Birthday == request.Birthday);

            if (employee is not null)
            {
                throw new Exception($"Employee with {{ FIO = '{request.FIO}, Birthday = '{request.Birthday}' is already exist");
            }

            employee = new Employee
            {
                FIO = request.FIO!,
                Birthday = request.Birthday!.Value
            };

            FillPositionsByIds(employee, request.Positions);
            var entry = DbContext.Employees.Add(employee);

            await DbContext.SaveAsync();

            return entry.Entity.Id;
        }

        private void FillPositionsByIds(Employee entity, IEnumerable<int> ids)
        {
            if (ids.Any())
            {
                entity.Positions.Clear();
                var positions = DbContext.Positions.Where(x => ids.Contains(x.Id));
                foreach(var position in positions)
                {
                    entity.Positions.Add(position);
                }
            }
        }
    }
}
