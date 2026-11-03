using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Models;

namespace SimpleBlogger.Business.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetSingleTagAsync(int tagId);
        Task<Tag> AssignTagAsync(int postId, string tagName);
    }
}
