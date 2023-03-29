﻿namespace API.Model
{
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Grade { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
