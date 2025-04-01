using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data;
using ToDo.Api.Entities;

namespace ToDo.Api.Services
{
    public class ToDoService : IToDoService
    {
        readonly ToDoListContext _context;

        public ToDoService(ToDoListContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoTask>> GetAllTodos(int page, int pageSize)
        {
            return await _context.Tasks
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<List<ToDoTask>> FindTodosAsync(int page, int pageSize, string searchName)
        {
            return await _context.Tasks.Where(task =>
                         task.Name.ToLower().Contains(searchName.ToLower()))
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
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

        public async Task UpdateToDoAsync(ToDoTask currTask, ToDoTask updatedTask)
        {
            updatedTask.Id = currTask.Id;
            _context.Entry(currTask).CurrentValues.SetValues(updatedTask);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteToDoAsync(int id)
        {
            _context.Tasks.Where(task => task.Id == id).ExecuteDelete();
            await _context.SaveChangesAsync();
        }
    }
}
