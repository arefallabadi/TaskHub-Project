using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskHub.API.DTOs.Comment;
using TaskHub.API.DTOs.Pagination;
using TaskHub.API.Services.Interfaces;

namespace TaskHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        private string GetUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? "User";
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PaginationParams pagination, [FromQuery] int? taskId)
        {
            var userId = GetUserId();
            var userRole = GetUserRole();
            var comments = _commentService.GetAll(pagination, taskId, userId, userRole);
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            _commentService.Create(dto, userId);
            return Ok(new { Message = "Comment created successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetUserId();
            var userRole = GetUserRole();

            try
            {
                var deleted = _commentService.Delete(id, userId, userRole);
                if (!deleted)
                    return NotFound(new { Message = "Comment not found." });
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { Message = ex.Message });
            }
        }
    }
}
