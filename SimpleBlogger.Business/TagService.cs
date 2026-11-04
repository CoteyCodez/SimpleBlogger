using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Business.Interfaces;
using SimpleBlogger.Data;
using SimpleBlogger.Models;

namespace SimpleBlogger.Business
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;

        public TagService(ApplicationDbContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }
        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetSingleTagAsync(int tagId)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
        }

        public async Task<Tag> AssignTagAsync(int postId, string tagName)
        {
            //Check if post exists

            var post = await _postService.GetPostWithTagAndCommentsAsync(postId) ?? throw new Exception("Post does not exist!");

            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                ?? throw new Exception("Tag does not exist!");

            //Check if the tag already exists on post
            var alreadyLinked = await _context.PostTags.AnyAsync(pt => pt.PostId == postId && pt.TagId == tag.Id);

            //Link post and tag
            if (!alreadyLinked)
            {
                _context.PostTags.Add(new PostTag { PostId = postId, TagId = tag.Id });
                await _context.SaveChangesAsync();
            }

            return tag;
        }
    }
}
