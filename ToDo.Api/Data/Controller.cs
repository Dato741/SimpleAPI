using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Entities;

namespace ToDo.Api.Data
{
    [Route("todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this._toDoService = toDoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskList()
        {
            List<ToDoTask> tasks = await _toDoService.GetAllTodos();
            return Ok(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskListSorted(string sortBy = "duedate", 
                                                           bool asc = false)
        {
            List<ToDoTask> tasks = await _toDoService.GetAllTodos();

            if (sortBy == "status")
            {
                tasks.Sort((a, b) =>
                {
                    if (a.IsCompleted == b.IsCompleted)
                        return a.DueDate.CompareTo(b.DueDate);

                    if (asc)
                        return a.IsCompleted.CompareTo(b.IsCompleted);
                    else
                        return b.IsCompleted.CompareTo(a.DueDate);
                });
            }
            else if (sortBy == "priority")
            {
                tasks.Sort((a, b) =>
                {
                    if (a.Priority == b.Priority)
                        return a.DueDate.CompareTo(b.DueDate);

                    if (asc)
                        return a.Priority.CompareTo(b.Priority);
                    else
                        return b.Priority.CompareTo(a.DueDate);
                });
            }
            else if (sortBy == "duedate")
            {
                tasks.Sort((a, b) =>
                {
                    if (a.DueDate == b.DueDate)
                        return a.Priority.CompareTo(b.Priority);

                    if (asc)
                        return a.DueDate.CompareTo(b.DueDate);
                    else
                        return b.DueDate.CompareTo(a.DueDate);
                });
            }

            return Ok(tasks);
        }

        [HttpGet("searchByName")]
        public async Task<IActionResult> SearchTasks(string searchName)
        {
            List<ToDoTask> tasks = await _toDoService.FindTodosAsync(searchName);
            
            if (tasks.Count == 0) return NotFound();

            return Ok(tasks);
        }

        [HttpGet("searchById")]
        public async Task<IActionResult> SearchTasks(int id)
        {
            ToDoTask task = await _toDoService.FindTodosAsync(id);

            if (task is null) return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(ToDoTask todo)
        {
            await _toDoService.AddTodoAsync(todo);

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, ToDoTask updatedTask)
        {
            ToDoTask currTask = await _toDoService.FindTodosAsync(id);

            if (currTask is not null)
            {
                await _toDoService.UpdateToDoAsync(currTask, updatedTask);
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _toDoService.DeleteToDoAsync(id);
            return NoContent();
        }
    }
}
