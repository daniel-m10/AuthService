using AuthService.Api.Domain.Interfaces;
using AuthService.Api.Features.RegisterUser;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapRegisterUserEndpoint();

app.Run();
