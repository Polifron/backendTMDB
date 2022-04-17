using backend.Controllers;
using backend.Controllers.Repositories;
using backend.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;

}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

});

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
    policy =>
    {
        policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("https://localhost:3000");
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var loggler = services.GetRequiredService<ILogger<Program>>();
    loggler.LogError(ex, "Error occured on migration");
}
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
