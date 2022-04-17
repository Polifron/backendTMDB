namespace backend.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int MovieId { get; set; }
        public int? ParentId { get; set; }
        public string UserName { get; set; }
    }
}
