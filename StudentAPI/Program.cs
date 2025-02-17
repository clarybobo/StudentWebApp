using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using StudentAPI.Data;
using StudentAPI.Data.Interfaces;
using StudentAPI.Data.Repositories;
using StudentAPI.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStudent, StudentRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific", policy =>
    {
        policy.WithOrigins("http://localhost:7170", "http://localhost:7246", "https://studentwebappnewclient.azurewebsites.net")
              .AllowAnyHeader()
              .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header");
    });
});


var app = builder.Build();

app.UseCors("AllowSpecific");

// Seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var appDbContext = services.GetRequiredService<AppDbContext>();
    var studentSeeder = new StudentSeeder(appDbContext);
    await studentSeeder.SeedStudents();
}

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
