using ToDo.Api.Entities;
using ToDo.Api.Dtos;

namespace ToDo.Api.Mappings
{
    public static class ToDoTaskMapper
    {
        public static ToDoTaskDto ToTaskDto (ToDoTask task)
        {
            return new ToDoTaskDto
            {
                Name = task.Name,
                Status = (task.IsCompleted) ? "Completed" : "Pending",
                Priority = task.Priority switch
                {
                    1 => "High",
                    2 => "Medium",
                    _ => "Low"
                },
                DueDate = task.DueDate.ToString("dd/MM/yyyy")
            };
        }
    }
}
