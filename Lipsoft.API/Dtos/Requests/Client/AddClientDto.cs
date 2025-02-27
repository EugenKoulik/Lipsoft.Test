namespace Lipsoft.API.Dtos.Requests.Client;

public record AddClientDto
{
    public string? FullName { get; init; }
    public int Age { get; init; }
    public string? Workplace { get; init; }
    public string? Phone { get; init; }
}

public static class AddClientDtoExtensions
{
    public static Data.Models.Client ToClient(this AddClientDto dto)
    {
        return new Data.Models.Client
        {
            FullName = dto.FullName,
            Age = dto.Age,
            Workplace = dto.Workplace,
            Phone = dto.Phone
        };
    }
}