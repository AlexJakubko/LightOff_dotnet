using LightsOffCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOff.LightsOffCore.Service
{
    public interface ICommentService
    {
        void AddComment(Comment comment);

        IList<Comment> GetLastComments();

        void ClearComments();

    }
}
