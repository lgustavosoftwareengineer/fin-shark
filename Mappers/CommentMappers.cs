using FinShark.Dtos.Comment;
using FinShark.Models;

namespace FinShark.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment) {
            return new CommentDto {
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                Id = comment.Id,
                StockId = comment.StockId,
                Title = comment.Title
            };
        }
    }
}