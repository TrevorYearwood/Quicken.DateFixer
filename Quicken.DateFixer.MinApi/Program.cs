using System.Security.Claims;
using Quicken.DateFixer.MinApi.Extensions;
using Quicken.DateFixer.Services;
using Quicken.DateFixer.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileService, LocalFileService>();
builder.Services.AddScoped<IQuickenService, QuickenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.RegisterQuickenEndpoints();

app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name} My secret").RequireAuthorization();

app.Run();

