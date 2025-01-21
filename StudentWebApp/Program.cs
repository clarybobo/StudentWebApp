using Microsoft.Net.Http.Headers;
using StudentWebApp.Data.Interfaces;
using StudentWebApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IStudentService, StudentService>();

// TODO: l�gg in r�tt Uri efter publikation

//Localhost som inte funkar
//builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
//client.BaseAddress = new Uri("https://localhost:7277/api/"));

//Localhost som funkar, men den ska ligga i appsettings ist.
//builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
//client.BaseAddress = new Uri("https://localhost:7246/"));

//GAMMAL - numera �r BaseUrl BORTA fr�n ApiSettings!! 
//builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
//{
//    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
//    if (string.IsNullOrEmpty(baseUrl))
//    {
//        throw new InvalidOperationException("BaseUrl �r inte konfigurerad i appsettings.json.");
//    }
//    client.BaseAddress = new Uri(baseUrl);
//});

// H�rdkodad BaseUrl
builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{
    // Ange direkt BaseUrl h�r f�r API:t
    client.BaseAddress = new Uri("https://studentapi-app-new.calmtree-16028aa1.northeurope.azurecontainerapps.io"); // Exempel f�r lokal utveckling
    ;
});

// TODO: OK med AllowAnyOrigin p� CORS?? 
// API https://localhost:7246/
// WEBAPP https://localhost:7277/ 
// Azure 

// Gamla fungerande Cors 
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", policy =>
//    {
//        policy.AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader();
//    });
//});

//Nya Cors
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowedOrigins", policy =>
//    {
//        policy.WithOrigins(
//            "https://localhost:7246/", //API 
//            "https://localhost:7277/" //WebApp
//                                      //TODO: L�gg till origins f�r azure
//         )
//        .WithMethods("GET")
//        .WithHeaders("Content-Type");
//    });
//});


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Suzies CORS 
app.UseCors(policy =>
policy.WithOrigins("https://localhost:7246/", "https://localhost:7277/", "https://studentwebappclient20250120153311.azurewebsites.net")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//TODO: kommentera ut n�r nya cors ska anv�ndas
//app.UseCors("AllowedOrigins");


app.UseAuthorization();

app.MapRazorPages();

app.Run();
