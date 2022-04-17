using backend.Dtos;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments/movieId
        //("{movieId:int}")
        // [HttpGet("movieId/{id:int}")] // GET /api/test2/int/3
        // [HttpGet("int2/{id}")]  // GET /api/test2/int2/3
        // GET: api/Comments/movieId
        //("{movieId:int}")
        [HttpGet("{movieId:int}")]
        public async Task<ActionResult<List<Comment>>> GetComments(int movieId)
        {
            //List<Comment> commentList = await _context.Comments.Where(x => x.MovieId == movieId).Include(x => x.User).ToListAsync();
            //List<ComentsDto> comentDto = commentList.Select(x => new ComentsDto
            //{
            //    Id = x.Id,
            //    Message = x.Message,
            //    ParentId = x.ParentId,
            //    MovieId = x.MovieId,
            //    UserName = x.User.Name
            //}).ToList();
            //return Ok(comentDto);
            //var result = ctx.News.Select(news => new
            //{
            //    news = news,
            //    Username = news.Author.Username
            //}).ToList();
            return await _context.Comments.Where(x => x.MovieId == movieId).ToListAsync();
            

        }


        // GET: api/Comments/5
        [HttpGet("{movieId:int}/{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("{movieId:int}")]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
            return Ok(comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
