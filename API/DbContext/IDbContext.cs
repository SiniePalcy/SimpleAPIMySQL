using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.DbContext
{
    public interface IDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Position> Positions { get; set; }
        Task SaveAsync();
    }
}
