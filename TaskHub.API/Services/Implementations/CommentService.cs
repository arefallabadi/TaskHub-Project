using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.EntityFrameworkCore;
using TaskHub.API.Data;
using TaskHub.API.DTOs.Comment;
using TaskHub.API.DTOs.Pagination;
using TaskHub.API.Entities;
using TaskHub.API.Repositories;
using TaskHub.API.Services.Interfaces;

namespace TaskHub.API.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly TaskHubDbContext _context;

        public CommentService(IRepository<Comment> commentRepository, TaskHubDbContext context)
        {
            _commentRepository = commentRepository;
            _context = context;
        }

        // Get all comments with optional pagination
        // Admin sees all, User sees comments for their assigned tasks
        public List<CommentDto> GetAll(PaginationParams pagination, int? taskId, int userId, string userRole)
        {
            var query = _context.Comments.Include(c => c.User).Include(c => c.Task).AsQueryable();

            // Filter by task if provided
            if (taskId.HasValue)
            {
                query = query.Where(c => c.TaskItemId == taskId.Value);
            }

            // User role: only show comments for tasks assigned to them
            if (userRole != "Admin")
            {
                query = query.Where(c => c.Task != null && c.Task.AssignedUserId == userId);
            }

            return query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    TaskId = c.TaskItemId,
                    UserId = c.AuthorId,
                    UserName = c.User != null ? c.User.Username : null,
                    CreatedAt = c.CreatedAt
                })
                .ToList();
        }

        // Create new comment
        public void Create(CommentDto dto, int userId)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                TaskItemId = dto.TaskId,
                AuthorId = userId, // Use userId from JWT, not from DTO
                CreatedAt = DateTime.UtcNow
            };

            _commentRepository.Add(comment);
        }

        // Delete comment - Admin can delete any, User can only delete own comments
        public bool Delete(int id, int userId, string userRole)
        {
            var comment = _context.Comments.Include(c => c.User).FirstOrDefault(c => c.Id == id);
            if (comment == null) return false;

            // User role: can only delete own comments
            if (userRole != "Admin" && comment.AuthorId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this comment.");
            }

            _commentRepository.Delete(comment);
            return true;
        }
    }
}
