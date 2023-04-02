using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotePad.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TestDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.MapGet("/", async (TodoDb db) => await db.Todos.ToListAsync());
app.MapGet("/{id}", async (int id,TodoDb db) => id).AddEndpointFilter(async (context, next) =>
{
    var id = context.GetArgument<int>(0);
    if (id > 10)
    {
        return Results.Problem("Id should be above 10");
    }
    return await next(context);
} );
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();