using  ToDo.Api.Data;

namespace ToDo.Api.Entities
{
    public record class ToDoTask
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
        public EPriority Priority { get; set; }
        public DateOnly DueDate { get; set; }
    }
}
