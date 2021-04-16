using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightsOff;
using LightsOff.LightsOffCore.Service;
using LightsOffCore.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LightsOffWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService = new CommentServiceFile();
        
        // GET: api/Comment
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return commentService.GetLastComments();
        }
    
        // POST: api/Score
        [HttpPost]
        public void Post([FromBody]Comment comment)
        {
            commentService.AddComment(comment);
        }
    }
}