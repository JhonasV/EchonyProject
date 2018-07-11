using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface ICommentReplyService
    {
        List<CommentReply> GetAllReplies();
        bool AddReply(CommentReply cr);
    }

    public class CommentReplyService : ICommentReplyService
    {
        private readonly EchonyDbContext _context;
        public CommentReplyService(EchonyDbContext context)
        {
            _context = context;
        }

        public List<CommentReply> GetAllReplies()
        {
              try
                {
                    return _context.CommentReply.Include(x => x.Publicaciones).Include(x => x.Usuario.Foto).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            
        }

        public bool AddReply(CommentReply cr)
        {           
                try
                {
                    _context.CommentReply.Add(cr);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }          
        }
    }
}
