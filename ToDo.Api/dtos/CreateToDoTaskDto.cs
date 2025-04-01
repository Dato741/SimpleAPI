namespace ToDo.Api.dtos
{
    public class CreateToDoTaskDto
    {
        public required string Name { get; set; }
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Low";
        public string DueDate { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
    }
}
