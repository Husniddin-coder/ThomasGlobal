using Application.DTO.AcceptDtos;
using Application.DTO.AccountDtos;
using Application.DTO.BasketDtos;
using Application.DTO.CommentDtos;
using Application.DTO.ImageDtos;
using Application.DTO.ProductDtos;
using Application.DTO.StoryDtos;
using AutoMapper;
using Domain.Models;
using System.Reflection.Emit;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AccountMapping();
            StoryMapping();
            BasketMapping();
            AcceptMapping();
            CommentMapping();
            ImageMapping();
            ProductMapping();
        }

        private void ProductMapping()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductCreateDto, Product>()
                .ForMember(d => d.Account, o => o.MapFrom(x => new Account { Id = x.AccountId }))
                .ForMember(d => d.Accept, o => o.MapFrom(x => new Accept { Title_accept = x.Product_accept.ToString(), Title_no_accept = x.Product_accept.ToString()}));
                
                
        }

        private void ImageMapping()
        {
            CreateMap<Image, ImageGetDto>();
            CreateMap<ImageCreateDto, Image>();
        }

        private void CommentMapping()
        {
            CreateMap<Comment, CommentGetDto>();
            CreateMap<CommentCreateDto, Comment>();
        }

        private void AcceptMapping()
        {
            CreateMap<Accept, AcceptGetDto>();
        }

        private void BasketMapping()
        {
            CreateMap<Basket, BasketGetDto>();
        }

        private void StoryMapping()
        {
            CreateMap<Story, StoryGetDto>();
            CreateMap<StoryCreateDto, Story>();
        }

        private void AccountMapping()
        {
            CreateMap<Account, AccountGetDto>()
                .ForMember(d=> d.Account_image,o=> o.MapFrom(x=> x.Image.FilePath));
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>()
                .ForMember(d=> d.Image, o=> o.MapFrom(x=> new Image { FilePath = x.Account_image }));
            //.ForMember(d => d.Products, o => o.MapFrom(x => x.ProductsIds.Select(x => new Product { Id = x })));
        }
    }
}
