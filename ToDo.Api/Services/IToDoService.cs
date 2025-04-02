using ToDo.Api.Entities;

namespace ToDo.Api.Services
{
    public interface IToDoService
    {
        Task<List<ToDoTask>> GetAllTodos(int page, int pageSize);
        Task<List<ToDoTask>> FindTodosAsync(string searchName, int page, int pageSize);
        Task<ToDoTask?> FindTodosAsync(int id);
        Task AddTodoAsync(ToDoTask todo);
        Task UpdateToDoAsync(ToDoTask currTask, ToDoTask updatedTask);
        Task DeleteToDoAsync(int id);
    }
}
