using ToDo.Api.Entities;

namespace ToDo.Api.Services
{
    public interface IToDoService
    {
        Task<List<ToDoTask>> GetAllTodos(int page, int pageSize);
        Task<List<ToDoTask>> FindTodosAsync(int page, int pageSize, string searchName);
        Task<ToDoTask> FindTodosAsync(int id);
        Task AddTodoAsync(ToDoTask todo);
        Task UpdateToDoAsync(ToDoTask currTask, ToDoTask updatedTask);
        Task DeleteToDoAsync(int id);
    }
}
