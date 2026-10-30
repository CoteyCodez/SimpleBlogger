using SimpleBlogger.Models;

namespace SimpleBlogger.Business
{
    public interface ICommentService
    {

        Task<IEnumerable<Comment>> GetAllCommentsOnPostAsync(int postId);
        Task<Comment> GetSingleCommentAsync(int commentId);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task<Comment> DeleteCommentAsync(int commentId);


    }
}
