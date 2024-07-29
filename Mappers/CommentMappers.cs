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

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto createDto, int stockId) {
            return new Comment {
                Title = createDto.Title,
                Content = createDto.Content,
                StockId = stockId,
            };
        }
    }
}