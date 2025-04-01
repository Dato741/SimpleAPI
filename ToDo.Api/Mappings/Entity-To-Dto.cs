using ToDo.Api.Entities;
using ToDo.Api.Dtos;
using ToDo.Api.dtos;

namespace ToDo.Api.Mappings
{
    public static class ToDoTaskMapper
    {
        public static ToDoTaskDto ToTaskDto (this ToDoTask task)
        {
            return new ToDoTaskDto
            {
                Id = task.Id,
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

        public static ToDoTask ToTaskEntity (this CreateToDoTaskDto task)
        {
            return new ToDoTask
            {
                Name = task.Name,
                IsCompleted = (task.Status == "Completed") ? true : false,
                Priority = task.Priority switch
                {
                    "High" => 1,
                    "Medium" => 2,
                    _ => 3
                },
                DueDate = DateOnly.Parse(task.DueDate)
            };
        }
    }
}
