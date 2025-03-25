

namespace ToDo.Api.Entities
{
    public record class ToDoTask
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
        public string Priority { get; set; } = "Low";
        public DateOnly DueDate { get; set; }
    }
}
