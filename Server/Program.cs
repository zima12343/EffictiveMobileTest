using DataAccess.Repository;
using DataAccess.Repository.Abstraction;
using DataAccess.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Server.Middleware;
using Services.DataGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IAreaRepository, AreaRepository>();
builder.Services.AddTransient<IDataGenerator, DataDenerator>();
builder.Services.AddTransient<ILoggerRepository, LoggerRepository>();

builder.Services.AddDbContext<OrdersContext>
            (options => options.UseSqlite(builder.Configuration["ConnectionString"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHeandler>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
