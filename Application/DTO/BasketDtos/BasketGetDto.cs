using Domain.Models;

namespace Application.DTO.BasketDtos
{
    public class BasketGetDto
    {
        public Guid Id { get; set; }

        public IList<Product>? Products { get; set; }
    }
}
