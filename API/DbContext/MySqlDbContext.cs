namespace API.DbContext
{
    public class MySqlDbContext : BaseDbContext
    {
        public MySqlDbContext(IConfiguration configuration)
            : base(configuration, "MySql")
        { }
    }
}
