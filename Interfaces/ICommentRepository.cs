using FinShark.Dtos.Comment;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);

        Task<Comment> CreateAsync(Comment commentModel);


        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateDto);
    }
}