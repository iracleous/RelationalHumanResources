using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RelationalHumanResources.Data;
using RelationalHumanResources.Models;
using RelationalHumanResources.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var optionsCon = builder.Configuration
           .GetConnectionString("MyConn");
builder.Services.AddDbContext<HrDbcontext>(options =>
        options.UseSqlServer(optionsCon));

// Apply pending migrations and create/update the database
var optionsBuilder = new DbContextOptionsBuilder<HrDbcontext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("MyConn"));
using (var dbContext = new HrDbcontext(optionsBuilder.Options))
{
    dbContext.Database.Migrate();
}



builder.Services.AddScoped<IHrService, HrService>();
builder.Services.AddScoped<IGenService<Employee, long>,
    GenService<Employee, long>>();
builder.Services.AddScoped<IGenService<Project, long>,
    GenService<Project, long>>();

// swagger step 1/2
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions
         .ReferenceHandler = ReferenceHandler.Preserve;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//swagger step 2/2
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
