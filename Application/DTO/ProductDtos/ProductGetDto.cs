using Application.DTO.AcceptDtos;
using Application.DTO.CommentDtos;
using Application.DTO.ImageDtos;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.ProductDtos
{
    public class ProductGetDto
    {
        public Guid Id { get; set; }

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

        public AcceptGetDto Accept { get; set; }

        public IList<CommentGetDto> Comments { get; set; }

        public IList<ImageGetDto> Images { get; set; }
    }
}
