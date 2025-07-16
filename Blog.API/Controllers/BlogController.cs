using Blog.Application.Features.BlogPosts.Commands;
using Blog.Application.Features.BlogPosts.Queries.GetAll;
using Blog.Application.Features.BlogPosts.Queries.GetById;
using Blog.Application.Features.BlogPosts.Queries.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _mediator.Send(new GetAllPosts());
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _mediator.Send(new GetPostsById(id));
            return Ok(post);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
        {
            var result = await _mediator.Send(new SearchPosts (title));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePost command)
        {
            var post = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] UpdatePost command)
        {
            if (postId != command.PostId)
                return BadRequest("Post ID mismatch.");
            var updatedPost = await _mediator.Send(command);
            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePost (id));
            return NoContent();
        }
    }
}
