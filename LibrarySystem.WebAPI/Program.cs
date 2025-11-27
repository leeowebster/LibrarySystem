using System.Reflection;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure;
using LibrarySystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbContext, LibraryContext>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "Library API V1");
    options.SupportedSubmitMethods(); //hide the try it out button
    options.DefaultModelsExpandDepth(-1); //Hide schemas from /swagger/index.html
});
app.MapControllers();


app.Run();
