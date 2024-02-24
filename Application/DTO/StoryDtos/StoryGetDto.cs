namespace Application.DTO.StoryDtos
{
    public class StoryGetDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Topic { get; set; }

        public string Story_Image { get; set; }

        public int Like { get; set; }
    }
}
