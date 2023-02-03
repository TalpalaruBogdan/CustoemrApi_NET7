using AutoMapper;
using CustomerService.DTOs;
using CustomerService.ExtensionMethods;
using CustomerService.Maps;
using CustomerService.Repository;
using CustomerService.Validators;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services
    .AddSingleton<IMapper>(CustomerMapper.Instance)
    .AddSingleton<IValidator<CustomerDTO>>(CustomerValidator.Instance)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase("TestDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureEndpoints();

app.Run();