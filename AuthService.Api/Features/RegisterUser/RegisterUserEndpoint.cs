using AuthService.Api.Domain.Common;
using AuthService.Api.Domain.Entities;
using AuthService.Api.Domain.Interfaces;
using FluentValidation;

namespace AuthService.Api.Features.RegisterUser
{
    public static class RegisterUserEndpoint
    {
        public static IEndpointRouteBuilder MapRegisterUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/register",
                async (
                    RegisterUserRequest request,
                    IValidator<RegisterUserRequest> validator,
                    IRegisterUserHandler handler) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new
                    {
                        Field = e.PropertyName,
                        Message = e.ErrorMessage
                    });

                    return Results.BadRequest(new { Errors = errors });
                }


                Result<User> result = await handler.HandleAsync(request);

                if (!result.ISuccess)
                {
                    return Results.Problem(
                        title: "Registration Failed",
                        detail: result.ErrorMessage,
                        statusCode: result.ErrorMessage == "Email is already taken."
                                        ? StatusCodes.Status409Conflict
                                        : StatusCodes.Status400BadRequest
                    );
                }

                return Results.Created($"/users/{result.Value?.Id}", new
                {
                    result.Value?.Id,
                    result.Value?.Email
                });
            })
                .WithTags("Auth")
                .WithName("RegisterUser")
                .Accepts<RegisterUserRequest>("application/json")
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status409Conflict);

            return app;
        }
    }
}
