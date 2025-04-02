using System.Collections.Immutable;
using ToDo.Api.Data;
using ToDo.Api.Controllers;
using ToDo.Api.Services;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ToDoApp");
builder.Services.AddSqlite<ToDoListContext>(connString);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<ToDoListContext>();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        HttpResponse response = context.Response;

        response.ContentType = "application/json";

        switch (exception)
        {
            case ArgumentException:
                response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await response.WriteAsJsonAsync(new
        {
            error = exception?.Message,
            statusCode = response.StatusCode
        });
    });
});


app.Run();
