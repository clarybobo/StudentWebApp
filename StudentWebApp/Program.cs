using StudentWebApp.Data.Interfaces;
using StudentWebApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IStudentService, StudentService>();

// TODO: lägg in rätt Uri efter publikation

//Localhost som inte funkar
//builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
//client.BaseAddress = new Uri("https://localhost:7277/api/"));

//Localhost som funkar, men den ska ligga i appsettings ist.
//builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
//client.BaseAddress = new Uri("https://localhost:7246/"));

builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new InvalidOperationException("BaseUrl är inte konfigurerad i appsettings.json.");
    }
    client.BaseAddress = new Uri(baseUrl);
});



// TODO: OK med AllowAnyOrigin på CORS?? 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
