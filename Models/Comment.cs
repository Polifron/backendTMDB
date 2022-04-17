using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int MovieId { get; set; }
        public int? ParentId { get; set; }
        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
