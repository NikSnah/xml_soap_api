using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Use secrets to store sensitive data

builder.Configuration.AddUserSecrets<Program>();


// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

// Register the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
    sqlServerOptions => sqlServerOptions.CommandTimeout(120))
    );

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


app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
