using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebApi.Middleware;
using System.Reflection;

// Services
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});



// Configure
var app = builder.Build();

try
{
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
    DbInitializer.Initialize(context);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

