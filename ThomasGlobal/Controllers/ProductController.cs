using Application.DTO.ProductDtos;
using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ThomasGlobal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var imagesFolderPath = "images";
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            Product product = _mapper.Map<Product>(productCreate);

            List<Image> images = new List<Image>();

            foreach (var file in productCreate.image_files)
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Invalid file");

                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Get the file path for saving
                var filePath = Path.Combine("images", fileName);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Save the file path in the database
                var imageEntity = new Image
                {
                    FileName = fileName,
                    FilePath = filePath,
                    Product = product
                };
                images.Add(imageEntity);
            }
            List<Comment> comments = new List<Comment>();
            foreach (var comment in productCreate.Comments)
            {
                var commetary = new Comment
                {
                    Title = comment.Title,
                    Starts_count = comment.Starts_count,
                    Product = product
                };

                comments.Add(commetary);
            }

            product.Images = images;
            product.Comments = comments;

            product = await _productRepository.CreateAsync(product);

            if (product == null) return BadRequest("Product Creation failed");

            ProductGetDto productGetDto = _mapper.Map<ProductGetDto>(product);

            return Created("", productGetDto);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid productId)
        {
            Product? product = await _productRepository.GetAllAsync(x => x.Id == productId).Result
                .Include(x => x.Images).FirstOrDefaultAsync();
            if (product == null) return BadRequest("Product not found");

            foreach (var imageEntity in product.Images)
            {
                if (System.IO.File.Exists(imageEntity.FilePath))
                {
                    System.IO.File.Delete(imageEntity.FilePath);
                }
            }
            bool result = await _productRepository.DeleteAsync(productId);

            return Ok(result);
        }
    }
}
