using TaskHub.API.DTOs.Comment;
using TaskHub.API.DTOs.Pagination;

namespace TaskHub.API.Services.Interfaces
{
	public interface ICommentService
	{
        List<CommentDto> GetAll(PaginationParams pagination, int? taskId, int userId, string userRole);
        void Create(CommentDto dto, int userId);
        bool Delete(int id, int userId, string userRole);
	}
}
