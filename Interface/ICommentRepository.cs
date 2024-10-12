using APISYMBOL.Helper;
using APISYMBOL.Model;

namespace APISYMBOL.Interface
{
    public interface ICommentRepository
        
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject query);
        Task<Comment> CreatAsync(Comment Commentmodel);
        Task<Comment?> GetById(int Id);
        Task<Comment?> UpdateAsync(int Id, Comment commentModel);
        Task<Comment?> DeleteAsync(int Id);
        
    }
}
