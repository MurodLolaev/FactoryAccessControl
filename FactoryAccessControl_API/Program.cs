using FactoryAccessControl.Application.InterfaceRepository;
using FactoryAccessControl.Application.InterfaceService;
using FactoryAccessControl.Application.Services;
using FactoryAccessControl.Infrastructure;
using FactoryAccessControl.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//подключение бд зависимости
builder.Services.AddDbContext<FactoryDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FactoryDb")));

// Add services to the container.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
