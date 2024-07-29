using FinShark.Data;
using FinShark.Interfaces;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ApplicationDBContext _context;


        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();

            return comments;
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            var comment = _context.Comments.FirstOrDefaultAsync(c => c.Id == id);


            return comment;
        }
    }
}