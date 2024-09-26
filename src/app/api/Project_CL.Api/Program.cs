using Microsoft.EntityFrameworkCore;
using Project_CL.Data.context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<Project_CL_Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
