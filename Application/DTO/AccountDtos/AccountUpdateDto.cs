namespace Application.DTO.AccountDtos
{
    public class AccountUpdateDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public bool Delivery { get; set; }

        public string Region { get; set; }

        public string Company_name { get; set; }

        public string Account_image { get; set; }

        //public IList<Guid>? ProductsIds { get; set; }
    }
}
