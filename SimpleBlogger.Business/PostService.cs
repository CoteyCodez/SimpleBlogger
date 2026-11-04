using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Business.Interfaces;
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

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await GetPostWithTagAndCommentsAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post; 
        }

        public async Task<Post> GetPostWithTagAndCommentsAsync(int postId)
        {
            return await _context.Posts.Include(p => p.PostTag)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }
        public Task<Post> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetPostsWithTagAndCommentsAsync(string? filter = null)
        {
            var postsWithTags = new List<Post>();


            var query = await _context.Posts.Include(p => p.PostTag).ThenInclude(pt => pt.Tag)
                        .Include(p => p.Comments)
                        .ToListAsync();
           
            if (!string.IsNullOrEmpty(filter))
                query  = query.Where(t => t.PostTag?.Tag.Name == filter).ToList();

            return query;
        }

        public async Task<IEnumerable<Post>> FilterPosts(IEnumerable<Post> posts, string filter)
        {
            var tagList = await _context.Tags.ToListAsync(); 
            //tagList.Append(tagList => new Tag (name))
            IEnumerable<Post> filteredList = (IEnumerable<Post>)posts.Select(t => t.PostTag.Tag.Name == filter);

            return filteredList;
        }
    }
}
