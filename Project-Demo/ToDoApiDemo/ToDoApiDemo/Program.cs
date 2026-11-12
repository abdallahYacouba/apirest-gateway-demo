using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApiDemo.DataContext;
using ToDoApiDemo.Model.Entities;
using ToDoApiDemo.Model.ModelWrapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<TodoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:DBConnectionString"]));

var app = builder.Build();

app.MapGet("/items",
    async Task<Ok<IEnumerable<TodoItemDto>>>
    (TodoDbContext context, IMapper mapper) =>
{
    var resul = await context.Todos.ToListAsync();  
    return TypedResults.Ok(mapper.Map<IEnumerable<TodoItemDto>>(await context.Todos.ToListAsync()));
});


app.MapGet("/items/complete", async (TodoDbContext context) => {
    var data = await context.Todos.Where(t => t.IsComplete).ToListAsync();
    return TypedResults.Ok(data);   
});
app.MapGet("/items/{id}", async (int id, TodoDbContext context) 
    => { 
    var results = await context.Todos.FindAsync(id)
        is TodoItem todo
            ? Results.Ok(todo)
            : Results.NotFound();
    return results;
});
app.MapPost("/items", async (TodoItem todo, TodoDbContext context) =>
{
    context.Todos.Add(todo);
    await context.SaveChangesAsync();

    return Results.Created($"/items/{todo.Id}", todo);
});
app.MapPut("/items/{id}", async (int id, TodoItem inputTodo, TodoDbContext context) =>
{
    var todo = await context.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Title = inputTodo.Title;
    todo.IsComplete = inputTodo.IsComplete;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/items/{id}", async (int id, TodoDbContext context) =>
{
    if (await context.Todos.FindAsync(id) is TodoItem todo)
    {
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.Run();
