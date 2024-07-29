using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);
    }
}