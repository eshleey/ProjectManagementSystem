namespace PMSWPF.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
