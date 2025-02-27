namespace Lipsoft.API.Dtos.Requests.Client;

public class UpdateClientDto
{
    public string? FullName { get; init; }
    public int Age { get; init; }
    public string? Workplace { get; init; }
    public string? Phone { get; init; }
}

public static class UpdateClientDtoExtensions
{
    public static Data.Models.Client ToClient(this UpdateClientDto dto, long id)
    {
        return new Data.Models.Client
        {
            Id = id,
            FullName = dto.FullName,
            Age = dto.Age,
            Workplace = dto.Workplace,
            Phone = dto.Phone
        };
    }
}