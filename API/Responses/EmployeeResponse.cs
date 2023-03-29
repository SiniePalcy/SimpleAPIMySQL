namespace API.Responses
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime Birthday { get; set; }
        public List<PositionResponse> Positions { get; set; }
    }
}
