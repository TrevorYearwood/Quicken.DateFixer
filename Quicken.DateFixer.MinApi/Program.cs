using System.Security.Claims;
using Quicken.DateFixer.MinApi.Extensions;
using Quicken.DateFixer.Services;
using Quicken.DateFixer.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ReactApp",
    policy =>
            policy.WithOrigins(builder.Configuration["ReactAppUrl"]!)
                   .AllowAnyHeader()
                   .AllowAnyMethod());
    });

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileService, CloudFileService>();
builder.Services.AddScoped<IQuickenService, QuickenService>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors("ReactApp");

app.RegisterQuickenEndpoints();
app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name} My secret").RequireAuthorization();

app.Run();

