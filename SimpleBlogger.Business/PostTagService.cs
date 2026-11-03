using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Business.Interfaces;
using SimpleBlogger.Data;
using SimpleBlogger.Models;

namespace SimpleBlogger.Business.Interfaces
{
    public class PostTagService : IPostTagService
    {
        private readonly ApplicationDbContext _context;
        public PostTagService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PostTag> CreatePostTagAsync(PostTag postTag)
        {
            _context.PostTags.Add(postTag);
            await _context.SaveChangesAsync();
            return postTag; 
        }

        public async Task<PostTag> DeletePostTagAsync(int id)
        {
            var postTag = await _context.PostTags.FirstOrDefaultAsync(pt => pt.Id == id);
            _context.PostTags.Remove(postTag);
            await _context.SaveChangesAsync();
            return postTag;
        }

        //public async Task<PostTag> GetTagForPost(int id)
        //{
        //    var postTag = await _context.PostTags.FirstOrDefaultAsync(pt => pt.Id == id);
        //    _context.PostTags.Remove(postTag);
        //    await _context.SaveChangesAsync();
        //    return postTag;
        //}


    }
}
