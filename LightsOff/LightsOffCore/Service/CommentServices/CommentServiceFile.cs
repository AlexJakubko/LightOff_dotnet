using LightsOff.LightsOffCore.Service;
using LightsOffCore.Entity;
using LightsOffCore.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LightsOff
{
    public class CommentServiceFile : ICommentService
    {

        public void AddComment(Comment comment)
        {
            if (comment == null) throw new ServiceException("Comment cant be null!");
            if (comment.Name == null && comment.Message == null) throw new ServiceException("Message or Name is null !");

            using (var db = new LightsOffDbContext())
            {
                db.Add(comment);
                db.SaveChanges();
            }

        }

        public IList<Comment> GetLastComments()
        {
            using (var db = new LightsOffDbContext())
            {
                return db.Comments.OrderByDescending(s => s.TimeOfComment).Take(3).ToList();
            }
        }


        public void ClearComments()
        {
            using (var db = new LightsOffDbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Comments");
            }
        }
    }
}
       