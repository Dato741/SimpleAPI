using ToDo.Api.Entities;

namespace ToDo.Api.Data
{
    public interface IToDoService
    {
        Task<List<ToDoTask>> GetAllTodos();
        Task<List<ToDoTask>> FindTodosAsync(string searchName);
        Task<ToDoTask> FindTodosAsync(int id);
        Task AddTodoAsync(ToDoTask todo);
        Task UpdateToDoAsync(ToDoTask currTask, ToDoTask updatedTask);
        Task DeleteToDoAsync(int id);
    }
}
