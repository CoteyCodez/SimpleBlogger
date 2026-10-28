using SimpleBlogger.Models;

namespace SimpleBlogger.Business
{
    public interface IPostService
    {

        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetSinglePostAsync(int postId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(Post post);
        Task<Post> DeletePostAsync(int postId);


    }
}
