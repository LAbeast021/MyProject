using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ChiziBegooContext _context;

        public PostsController(ChiziBegooContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        //===============================================================================================================
        // GET: api/Posts/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUser(int userId)
        {
            var posts = await _context.Posts.Where(p => p.UserId == userId).ToListAsync();

            if (!posts.Any())
            {
                return Ok(new List<Post>());  // Return an empty list instead of a 404 error
            }

            return Ok(posts);
        }

        //===============================================================================================================

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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



        public class PostTestDto
        {
            public string Content { get; set; }
            public int UserId { get; set; }
        }
        // POST: api/Posts/test
        [HttpPost("newpost")]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostTestDto postDto)
        {
            if (postDto == null || string.IsNullOrEmpty(postDto.Content))
            {
                return BadRequest("Content is required.");
            }

            var newPost = new Post
            {
                UserId = postDto.UserId, // Ensure this is securely handled
                Content = postDto.Content,
                CreatedAt = DateTime.UtcNow,
                LikesCount = 0 // Initially, there are no likes
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = newPost.PostId }, newPost);
        }


        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post postDto)

        {
            var newpost = new Post
            {
                Content = postDto.Content,
                ImageUrl = postDto.ImageUrl,
                UserId = postDto.UserId, // Assuming you have a method to extract user id from the token
                CreatedAt = DateTime.UtcNow , 
                LikesCount = postDto.LikesCount,
            };
            _context.Posts.Add(newpost);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPost", new { id = newpost.PostId }, newpost);

        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
