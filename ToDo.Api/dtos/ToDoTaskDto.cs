using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.Dtos
{
    public record class ToDoTaskDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public required string DueDate { get; set; }
    }
}
