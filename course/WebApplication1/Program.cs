using EntityFrameworkCore.MySQL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionsString = builder.Configuration.GetConnectionString("RepairManagementConnestionString");
builder.Services.AddDbContext<RepairManagementDbContext>(options => options.UseMySql(connectionsString, ServerVersion.AutoDetect(connectionsString)));

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
