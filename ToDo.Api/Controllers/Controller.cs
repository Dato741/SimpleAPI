using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Dtos;
using ToDo.Api.Entities;
using ToDo.Api.Services;
using ToDo.Api.Mappings;
using ToDo.Api.dtos;
using ToDo.Api.Data;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("todos")]
    public class ToDoController : ControllerBase
    {
        readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        /// <summary>
        /// Retrieves existing tasks
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="sortBy">Parameter by which tasks should be sorted, on Default no sorting is applied</param>
        /// <param name="asc">Whether sorted info should be in ascending order or not</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTaskList(int page = 1, int pageSize = 10, ESortTypes sortBy = ESortTypes.None, 
                                                           bool asc = false)
        {
            List<ToDoTask> tasks = await _toDoService.GetAllTodos(page, pageSize);

            switch(sortBy)
            { 
                case ESortTypes.Status:
                    tasks.Sort((a, b) =>
                    {
                        if (a.IsCompleted == b.IsCompleted)
                            return a.DueDate.CompareTo(b.DueDate);

                        if (asc)
                            return a.IsCompleted.CompareTo(b.IsCompleted);
                        else
                            return b.IsCompleted.CompareTo(a.IsCompleted);
                    });
                break;

                case ESortTypes.Priority:
                    tasks.Sort((a, b) =>
                    {
                        if (a.Priority == b.Priority)
                            return a.DueDate.CompareTo(b.DueDate);

                        if (asc)
                            return a.Priority.CompareTo(b.Priority);
                        else
                            return b.Priority.CompareTo(a.Priority);
                    });
                    break;

                case ESortTypes.DueDate:
                    tasks.Sort((a, b) =>
                    {
                        if (a.DueDate == b.DueDate)
                            return a.Priority.CompareTo(b.Priority);

                        if (asc)
                            return a.DueDate.CompareTo(b.DueDate);
                        else
                            return b.DueDate.CompareTo(a.DueDate);
                    });
                    break;
            }

            List<ToDoTaskDto> taskDtos = tasks.Select(task => task.ToTaskDto()).ToList();

            return Ok(taskDtos);
        }

        /// <summary>
        /// Function to search task(s) by name  (full name not required)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="searchName">Name or part of name of the task</param>
        /// <returns></returns>
        [HttpGet("searchByName")]
        public async Task<IActionResult> SearchTasks(string searchName, int page = 1, int pageSize = 10)
        {
            List<ToDoTask> tasks = await _toDoService.FindTodosAsync(searchName, page, pageSize);
            
            if (tasks.Count == 0) return NotFound();

            List<ToDoTaskDto> taskDtos = tasks.Select(task => task.ToTaskDto()).ToList();

            return Ok(taskDtos);
        }

        /// <summary>
        /// Function to retrieve specific task by its id
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> SearchTasks(int id)
        {
            ToDoTask? task = await _toDoService.FindTodosAsync(id);

            if (task is null) return NotFound();

            return Ok(task.ToTaskDto());
        }

        /// <summary>
        /// Creates instance of a task
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ToDoTaskDto>> CreateTask([FromBody] CreateToDoTaskDto todo)
        {
            ToDoTask task = todo.ToTaskEntity();

            await _toDoService.AddTodoAsync(task);

            ToDoTaskDto taskDto = task.ToTaskDto();

            return CreatedAtAction(nameof(SearchTasks), new {id = taskDto.Id}, taskDto);
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="id">Task id</param>
        /// <param name="updatedTask">Property values which should be inserted in selected task</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, CreateToDoTaskDto updatedTask)
        {
            ToDoTask? currTask = await _toDoService.FindTodosAsync(id);

            if (currTask is null)
                return NotFound();

            await _toDoService.UpdateToDoAsync(currTask, updatedTask.ToTaskEntity());
            
            return NoContent();
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _toDoService.DeleteToDoAsync(id);
            return NoContent();
        }
    }
}
