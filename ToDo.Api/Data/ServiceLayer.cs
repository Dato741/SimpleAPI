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

        //search method overload with ID
        public async Task<ToDoTask> FindTodosAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
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

        public async Task DeleteToDoAsync(int id)
        {
            _context.Tasks.Where(task => task.Id == id).ExecuteDelete();
            await _context.SaveChangesAsync();
        }
    }
}
