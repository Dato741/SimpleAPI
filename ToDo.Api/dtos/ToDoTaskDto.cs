using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.Dtos
{
    public record class ToDoTaskDto
    {
        public required string Name { get; set; }
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Low";
        public string DueDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    }
}
