using Microsoft.AspNetCore.Http;

namespace Application.DTO.StoryDtos;

public class StoryCreateDto
{
    public string Title { get; set; }

    public string Topic { get; set; }

    public int Like { get; set; }

    public Guid AccountId { get; set; }

    public IFormFile ImageFile { get; set; }
}
