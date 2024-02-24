using Application.DTO.CommentDtos;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.ProductDtos
{
    public class ProductCreateDto
    {
        public double Amount { get; set; }

        public int Availability_count { get; set; }

        public string Code { get; set; }

        [Range(1, 14)]
        public int Region { get; set; }

        public string Company { get; set; }

        public double Discount_price { get; set; }

        public double Price { get; set; }

        public int Sales_count { get; set; }

        public string Count_massa { get; set; }

        public bool Product_accept { get; set; }

        public Guid AccountId { get; set; }

        public IList<IFormFile> image_files { get; set; }

        public IList<CommentCreateDto> Comments { get; set; } = new List<CommentCreateDto>();
    }
}
