using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Dtos;
using ToDo.Api.Entities;
using ToDo.Api.Services;
using ToDo.Api.Mappings;
using ToDo.Api.dtos;

namespace ToDo.Api.Controllers
{
    [Route("todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskList(int page, int pageSize, string sortBy = "", 
                                                           bool asc = false)
        {
            List<ToDoTask> tasks = await _toDoService.GetAllTodos(page, pageSize);

            if (sortBy == "status")
            {
                tasks.Sort((a, b) =>
                {
                    if (a.IsCompleted == b.IsCompleted)
                        return a.DueDate.CompareTo(b.DueDate);

                    if (asc)
                        return a.IsCompleted.CompareTo(b.IsCompleted);
                    else
                        return b.IsCompleted.CompareTo(a.IsCompleted);
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
                        return b.Priority.CompareTo(a.Priority);
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

            List<ToDoTaskDto> taskDtos = tasks.Select(task => task.ToTaskDto()).ToList();

            return Ok(taskDtos);
        }

        [HttpGet("searchByName")]
        public async Task<IActionResult> SearchTasks(int page, int pageSize, string searchName)
        {
            List<ToDoTask> tasks = await _toDoService.FindTodosAsync(page, pageSize, searchName);
            
            if (tasks.Count == 0) return NotFound();

            List<ToDoTaskDto> taskDtos = tasks.Select(task => task.ToTaskDto()).ToList();

            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SearchTasks(int id)
        {
            ToDoTask task = await _toDoService.FindTodosAsync(id);

            if (task is null) return NotFound();

            return Ok(task.ToTaskDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateToDoTaskDto todo)
        {
            ToDoTask task = todo.ToTaskEntity();

            await _toDoService.AddTodoAsync(task);

            ToDoTaskDto taskDto = task.ToTaskDto();

            return CreatedAtAction(nameof(SearchTasks), new {id = taskDto.Id}, taskDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, CreateToDoTaskDto updatedTask)
        {
            ToDoTask currTask = await _toDoService.FindTodosAsync(id);

            if (currTask is null)
                return NotFound();

            await _toDoService.UpdateToDoAsync(currTask, updatedTask.ToTaskEntity());
            
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
