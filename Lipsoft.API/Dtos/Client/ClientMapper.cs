using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.Client;

public class ClientMapper
{
    public static ClientModel ToClientModel(ClientDto dto)
    {
        return new ClientModel
        {
            Id = dto.Id,
            FullName = dto.FullName,
            Age = dto.Age,
            Workplace = dto.Workplace,
            Phone = dto.Phone
        };
    }
}