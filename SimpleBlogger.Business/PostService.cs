using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Data;
using SimpleBlogger.Models;
using SimpleBlogger.Business.Interfaces;

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

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await GetPostWithTagAndCommentsAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post; 
        }

        public async Task<Post> GetPostWithTagAndCommentsAsync(int postId)
        {
            return await _context.Posts.Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }
        public Task<Post> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetPostsWithTagAndCommentsAsync()
        {
            var postsWithTags = await _context.Posts.Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .ToListAsync();

            return postsWithTags;
        }
    }
}
