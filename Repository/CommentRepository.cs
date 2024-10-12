using APISYMBOL.Data;
using APISYMBOL.Helper;
using APISYMBOL.Interface;
using APISYMBOL.Model;
using Microsoft.EntityFrameworkCore;

namespace APISYMBOL.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context =context;
        }
        public async Task<Comment> CreatAsync(Comment Commentmodel)
        {
            await _context.Comments.AddAsync(Commentmodel);
            await _context.SaveChangesAsync();
            return Commentmodel;
        }

        public async Task<Comment?> DeleteAsync(int Id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == Id);
            if(comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject query)
        {
            var comments=  _context.Comments.Include(c=>c.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol == query.Symbol);

            };
            if (query.IsDecsending ==true)
            {
                comments = comments.OrderByDescending(c => c.createOn);

            };

            return await comments.ToListAsync();
        
        }

        public async  Task<Comment?> GetById( int Id)
        {
            return await  _context.Comments.Include(c=>c.AppUser).FirstOrDefaultAsync(c=>c.Id==Id);
        }

        public async Task<Comment?> UpdateAsync(int Id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(Id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return existingComment;

        }
    }
}
