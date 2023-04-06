using ExcelFileBackgroundJob.Application.Interfaces;
using ExcelFileBackgroundJob.Application.Services;
using ExcelFileBackgroundJob.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddSingleton<FileProcessingService>();
builder.Services.AddHostedService<FileProcessingHostedService>();
var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(connstring);
});
var app = builder.Build();

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
