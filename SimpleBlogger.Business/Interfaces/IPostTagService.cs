using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Data;
using SimpleBlogger.Models;

namespace SimpleBlogger.Business.Interfaces
{
    public interface IPostTagService
    {
        Task<PostTag> CreatePostTagAsync(PostTag postTag);
        Task<PostTag> DeletePostTagAsync(int id);
        Task<PostTag> GetPostTagAsync(int postId);

    }
}
