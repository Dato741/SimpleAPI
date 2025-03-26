using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data;
using ToDo.Api.Entities;

namespace ToDo.Api.Data
{
    public class ToDoService
    {
        readonly ToDoListContext _context;

        public ToDoService(ToDoListContext context)
        {
            this._context = context;
        }

        public async Task<List<ToDoTask>> GetAllTodos()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<List<ToDoTask>> FindTodosAsync(string searchName)
        {
            return await _context.Tasks.Where(task =>
                         task.Name.Contains(searchName)).ToListAsync();
        }

        public async Task AddTodoAsync(ToDoTask todo)
        {
            _context.Tasks.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateToDoAsync(ToDoTask todo)
        {
            _context.Entry(todo).CurrentValues.SetValues(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteToDoAsync(ToDoTask todo)
        {
            _context.Tasks.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
