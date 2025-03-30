using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Entities;

namespace ToDo.Api.Data
{
    public class ToDoController : ControllerBase
    {
        readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService)
        {
            this._toDoService = toDoService;
        }

        public async Task<IActionResult> GetTaskList(string sortBy = "", bool asc = false)
        {
            List<ToDoTask> tasks = await _toDoService.GetAllTodos();

            if (sortBy == "status")
            {
                    tasks.Sort((a, b) =>
                    {
                        if (a.IsCompleted == b.IsCompleted)
                            return a.DueDate.CompareTo(b.DueDate);

                        return a.IsCompleted.CompareTo(b.IsCompleted);
                    });
            }

            return Ok(tasks);
        }

        public async Task<IActionResult> SearchTasks(string searchName)
        {
            List<ToDoTask> tasks = await _toDoService.FindTodosAsync(searchName);
            
            if (tasks.Count == 0) return NotFound();

            return Ok(tasks);
        }

        public async Task<IActionResult> CreateTask(ToDoTask todo)
        {
            await _toDoService.AddTodoAsync(todo);

            return Ok(todo);
        }

        public async Task<IActionResult> UpdateTask(int id, ToDoTask todo)
        {
            ToDoTask task = await _toDoService.FindTodosAsync(id);

            if (task is not null)
            {
                await _toDoService.UpdateToDoAsync(task);
            }
            
            return NoContent();
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            await _toDoService.DeleteToDoAsync(id);
            return NoContent();
        }
    }
}
