using Application.DTO.BasketDtos;
using Application.DTO.ProductDtos;
using Application.DTO.StoryDtos;

namespace Application.DTO.AccountDtos
{
    public class AccountGetDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public bool Delivery { get; set; }

        public string Region { get; set; }

        public string Company_name { get; set; }

        public string Account_image { get; set; }

        public IList<StoryGetDto>? Stories { get; set; }

        public BasketGetDto? Basket { get; set; }

        public IList<ProductGetDto>? Products { get; set; }
    }
}
