using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Data;
using SimpleBlogger.Models;

namespace SimpleBlogger.Business
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post; 
        }

        public Task<Post> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
        public async Task<Post> GetSinglePostAsync(int postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }
        public Task<Post> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
