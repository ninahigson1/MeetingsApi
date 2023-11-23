using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MeetingContext>();

builder.Services.AddCors(options => { options.AddDefaultPolicy( policy => 
    { policy.WithOrigins("http://localhost:5500"); }); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetService<MeetingContext>()!.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{

}