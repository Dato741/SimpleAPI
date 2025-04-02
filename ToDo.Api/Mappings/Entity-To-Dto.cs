using ToDo.Api.Entities;
using ToDo.Api.Dtos;
using ToDo.Api.dtos;

namespace ToDo.Api.Mappings
{
    public static class ToDoTaskMapper
    {
        // From Entity to Dto
        public static ToDoTaskDto ToTaskDto (this ToDoTask task)
        {
            return new ToDoTaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Status = (task.IsCompleted) ? "Completed" : "Pending",
                Priority = Enum.GetName(task.Priority)!,
                DueDate = task.DueDate.ToString("dd/MM/yyyy")
            };
        }

        // From CreateDto to Entity
        public static ToDoTask ToTaskEntity (this CreateToDoTaskDto task)
        {
            return new ToDoTask
            {
                Name = task.Name,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority,
                DueDate = task.DueDate
            };
        }
    }
}
