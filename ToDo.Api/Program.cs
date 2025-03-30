using System.Collections.Immutable;
using ToDo.Api.Data;
using ToDo.Api.Controllers;
using ToDo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ToDoApp");
builder.Services.AddSqlite<ToDoListContext>(connString);

builder.Services.AddControllers();
builder.Services.AddScoped<ToDoListContext>();
builder.Services.AddScoped<IToDoService, ToDoService>();

var app = builder.Build();
app.MapControllers();

app.Run();
