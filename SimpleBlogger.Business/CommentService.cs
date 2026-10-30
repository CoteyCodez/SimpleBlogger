using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Data;
using SimpleBlogger.Models;

namespace SimpleBlogger.Business
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment; 
        }
        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<Comment> DeleteCommentAsync(int commentId)
        {
            var comment = await GetSingleCommentAsync(commentId);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment; 
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsOnPostAsync(int postId)
        {
            return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
        }
        public async Task<Comment> GetSingleCommentAsync(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }
        public Task<Post> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
