using AdaCard.Api.Extensions;
using AdaCard.Api.Middlewares;
using AdaCard.Infra.Dependencies.Mediatr;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

SerilogExtensions.AddSerilogApi();
builder.Host.UseSerilog(Log.Logger);

Log.Information("Initializing application");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddPersistenceDependencies();
builder.Services.AddApplicationDepencies();
builder.Services.AddMediator();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddDefaultValuesToDB();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
