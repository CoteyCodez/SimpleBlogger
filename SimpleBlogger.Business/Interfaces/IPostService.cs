using SimpleBlogger.Models;

namespace SimpleBlogger.Business.Interfaces
{
    public interface IPostService
    {
        Task<Post> GetPostWithTagAndCommentsAsync(int postId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(Post post);
        Task<Post> DeletePostAsync(int postId);
        Task<List<Post>> GetPostsWithTagAndCommentsAsync(string? filter = null);
    }
}
