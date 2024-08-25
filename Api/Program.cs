using Core.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<RepoContext>(opt =>
    opt.UseSqlite("Data source=../Data/Repo.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Repository Pattern",
        Version = "v1",
        Description = "Books and Authors using Repository Pattern and Unit Of Work"
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBooks();
app.MapAuthors();

app.Run();
