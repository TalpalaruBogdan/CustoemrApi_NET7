using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text.Json;

namespace CustomerService.ExtensionMethods
{
    public static class CustomerExtensionMethods
    {
        public static void ConfigureEndpoints(this WebApplication app)
        {
            app.MapGet("/customers", async (ICustomerRepository repo, IMapper mapper, ILogger<Customer> logger) =>
            {
                logger.LogInformation("Calling GET /customers");
                var response = new ApiResponse();
                var customers = await repo.GetCustomers();
                response.Result = customers.Select(customer => mapper.Map<CustomerDTO>(customer)).ToList();
                logger.LogInformation(JsonSerializer.Serialize(response));
                return Results.Ok(response);
            })
            .WithName("GetCustomers")
            .WithOpenApi()
            .Produces<ApiResponse>(200);

            app.MapGet("/customer/{id}", async ([FromRoute] Guid id, IMapper mapper, ILogger<Customer> logger, ICustomerRepository repo) =>
            {
                logger.LogInformation($"Calling GET /customer/{id}");
                var response = new ApiResponse();

                var existingCustomer = await repo.GetCustomer(id);

                if (existingCustomer is not null)
                {
                    response.Result = mapper.Map<CustomerDTO>(existingCustomer);
                    logger.LogInformation(JsonSerializer.Serialize(response));

                    return Results.Ok(response);
                }
                else
                {
                    response.Errors = new string[1] { $"Customer with id {id} does not exist" };
                }
                logger.LogInformation(JsonSerializer.Serialize(response));
                return Results.NotFound(response);
            })
            .WithName("GetCustomer")
            .WithOpenApi()
            .Produces<ApiResponse>(200)
            .Produces<ApiResponse>(404);


            app.MapPost("/customers", async ([FromBody] Customer customer, IValidator<CustomerDTO> validator, IMapper mapper, ILogger<Customer> logger, ICustomerRepository repo) =>
            {
                logger.LogInformation($"Calling POST /customer with payload {JsonSerializer.Serialize(customer)}");

                var response = new ApiResponse();
                var validationResult = validator.Validate(customer);
                if (!validationResult.IsValid)
                {
                    response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
                    logger.LogInformation(JsonSerializer.Serialize(response));
                    return Results.UnprocessableEntity(response);
                }

                var existingCustomer = await repo.GetCustomer(customer.Id);
                if (existingCustomer is not null)
                {
                    response.Errors = new string[1] { $"Customer with id {customer.Id} already exists" };
                    logger.LogInformation(JsonSerializer.Serialize(response));
                    return Results.Conflict(response);
                }

                await repo.CreateCustomer(customer);

                response.Result = mapper.Map<CustomerDTO>(customer);
                logger.LogInformation(JsonSerializer.Serialize(response));
                return Results.Created($"/customers/{customer.Id}", response);
            })
            .WithName("PostCustomer")
            .WithOpenApi()
            .Produces<ApiResponse>(200)
            .Produces<ApiResponse>(422)
            .Produces<ApiResponse>(409);

            app.MapDelete("/customer/{id}", async ([FromRoute] Guid id, ILogger<Customer> logger, ICustomerRepository repo) =>
            {
                logger.LogInformation($"Calling DELETE /customer/{id}");

                var existingCustomer = await repo.GetCustomer(id);
                if (existingCustomer is not null)
                {
                    await repo.DeleteCustomer(existingCustomer.Id);
                    return Results.Ok(new ApiResponse(null!, Array.Empty<string>()));
                }
                return Results.NotFound(new ApiResponse(null!, new string[1] { $"Customer with id {id} does not exist" }));
            })
            .WithName("DeleteCustomer")
            .WithOpenApi()
            .Produces<ApiResponse>(200)
            .Produces<ApiResponse>(404);

            app.MapPut("/customer/{id}", async ([FromRoute] Guid id, [FromBody] CustomerDTO customerDto, ILogger<Customer> logger, IMapper mapper, ICustomerRepository repo) =>
            {
                logger.LogInformation($"Calling PUT /customer/{id} with payload {JsonSerializer.Serialize(customerDto)}");

                var existingCustomer = await repo.GetCustomer(id);
                if (existingCustomer is not null)
                {
                    await repo.UpdateCustomer(id, customerDto);
                    return Results.Created($"/customers/{existingCustomer.Id}", new ApiResponse(customerDto!, Array.Empty<string>()));
                }
                return Results.NotFound(new ApiResponse(null!, new string[1] { $"Customer with id {id} does not exist" }));
            })
            .WithName("UpdateCustomer")
            .WithOpenApi()
            .Produces<ApiResponse>(200)
            .Produces<ApiResponse>(404);
        }
    }
}
