using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data;
using ToDo.Api.Entities;

namespace ToDo.Api.Endpoints
{
    public static class ToDoEndpoints
    {
        public static RouteGroupBuilder MapToDoEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("todos");

            //GET all   
            group.MapGet("/", (ToDoListContext dbContext) =>
                        dbContext.Tasks.AsNoTracking());

            //GET specific task(s)
            group.MapGet("/{id}", (ToDoListContext dbContext, int id) =>
            {
                ToDoTask? task = dbContext.Tasks.Find(id);

                return task is null ? Results.NotFound() : Results.Ok(task);
            }).WithName("ToDoList");

            //POST
            group.MapPost("/", (ToDoListContext dbContext, ToDoTask todo) =>
            {
                dbContext.Tasks.Add(todo);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute("ToDoList", new { id = todo.Id},todo);
            });

            //PUT
            group.MapPut("/{id}", (ToDoListContext dbContext, int id, ToDoTask updatedTask) =>
            {
                ToDoTask? existingTask = dbContext.Tasks.Find(id);

                if (existingTask is null) return Results.NotFound();

                updatedTask.Id = existingTask.Id;
                dbContext.Entry(existingTask).CurrentValues.SetValues(updatedTask);
                dbContext.SaveChanges();

                return Results.NoContent();
            });

            //DELETE
            group.MapDelete("/{id}", (ToDoListContext dbContext, int id) =>
            {
                dbContext.Tasks.Where(task => task.Id == id).ExecuteDelete();

                return Results.NoContent();
            });

            return group;
        }

    }
}
