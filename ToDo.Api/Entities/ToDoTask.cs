

namespace ToDo.Api.Entities
{
    public record class ToDoTask
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }
        public DateOnly DueDate { get; set; }
    }
}
