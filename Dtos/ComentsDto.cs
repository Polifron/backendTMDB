namespace backend.Dtos
{
    public class ComentsDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int MovieId { get; set; }
        public int? ParentId { get; set; }
        public string UserName { get; set; }
    }
}
