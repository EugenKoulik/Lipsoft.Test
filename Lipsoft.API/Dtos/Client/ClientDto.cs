namespace Lipsoft.API.Dtos.Client;

public record ClientDto(
    int Id,
    string? FullName,
    int Age,
    string? Workplace,
    string? Phone
);