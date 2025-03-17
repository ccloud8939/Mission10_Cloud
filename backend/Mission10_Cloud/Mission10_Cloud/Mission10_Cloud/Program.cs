using Microsoft.EntityFrameworkCore;
using Mission10_Cloud.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext <BowlingLeagueContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BowlersConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//this allows cross domain requests
//app.UseCors(x => x.WithOrigins("http://localhost:5000"));

app.UseCors(x => x
    .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost") // Allows any localhost port
    .AllowAnyMethod()
    .AllowAnyHeader());


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();