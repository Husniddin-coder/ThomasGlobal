using Microsoft.AspNetCore.Http;

namespace Application.DTO.AccountDtos;

public class AccountCreateDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public bool Delivery { get; set; }

    public string Region { get; set; }

    public string Company_name { get; set; }

    public IFormFile file { get; set; }

    //public IList<Guid>? ProductsIds { get; set; }
}
