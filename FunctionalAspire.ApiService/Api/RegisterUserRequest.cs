namespace FunctionalAspire.ApiService.Api;

public record RegisterUserRequest(
    string firstName,
    string lastName,
    string email,
    string password
);
