﻿namespace FunctionalAspire.ApiService.Api
{
  using FunctionalAspire.Domain;
  using FunctionalDdd;
  using ServiceLevelIndicators;

  public static class UserExt
  {
    public static void UseUserRoute(this WebApplication app)
    {
      RouteGroupBuilder userApi = app.MapGroup("/users")
        .AddServiceLevelIndicator();


      userApi.MapGet("/", () => "Hello Users");

      userApi.MapGet("/{name}", (string name) => $"Hello {name}").WithName("GetUserById");

      userApi.MapPost("/register", (RegisterUserRequest request) =>
          FirstName.TryCreate(request.firstName)
          .Combine(LastName.TryCreate(request.lastName))
          .Combine(EmailAddress.TryCreate(request.email))
          .Bind((firstName, lastName, email) => User.TryCreate(firstName, lastName, email, request.password))
          .ToOkResult());

      userApi.MapPost("/registerCreated", (RegisterUserRequest request) =>
          FirstName.TryCreate(request.firstName)
          .Combine(LastName.TryCreate(request.lastName))
          .Combine(EmailAddress.TryCreate(request.email))
          .Bind((firstName, lastName, email) => User.TryCreate(firstName, lastName, email, request.password))
          .Finally(
                  ok => Results.CreatedAtRoute("GetUserById", new RouteValueDictionary { { "name", ok.FirstName } }, ok),
                  err => err.ToErrorResult()));

      userApi.MapGet("/notfound/{id}", (int id) =>
          Result.Failure(Error.NotFound("User not found", id.ToString()))
          .ToOkResult());

      userApi.MapGet("/conflict/{id}", (int id) =>
          Result.Failure(Error.Conflict("Record has changed.", id.ToString()))
          .ToOkResult());

      userApi.MapGet("/forbidden/{id}", (int id) =>
          Result.Failure(Error.Forbidden("You do not have access.", id.ToString()))
          .ToOkResult());

      userApi.MapGet("/unauthorized/{id}", (int id) =>
          Result.Failure(Error.Unauthorized("You have not been authorized.", id.ToString()))
          .ToOkResult());

      userApi.MapGet("/unexpected/{id}", (int id) =>
          Result.Failure(Error.Unexpected("Internal server error.", id.ToString()))
          .ToOkResult());

    }

  }
}