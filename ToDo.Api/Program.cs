using System.Collections.Immutable;
using ToDo.Api.Data;
using ToDo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ToDoApp");
builder.Services.AddSqlite<ToDoListContext>(connString);

var app = builder.Build();

app.MapToDoEndpoints();

app.Run();
