using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LightsOffCore.Entity;

namespace LightsOff.LightsOffCore.Service.Tests
{
    [TestClass()]
    public class CommentServiceFileTests
    {
        ICommentService service = new CommentServiceFile();

        [TestMethod()]
        public void AddCommentTest()
        {
            service.AddComment(new Comment{ Name = "Alex", Message = "asdasdas" });
            service.AddComment(new Comment{ Name = "Duri", Message = "asdasdascmkodkadad" });

            service.ClearComments();

            Assert.AreEqual<int>(0, service.GetLastComments().Count);
        }

        [TestMethod()]
        public void GetLastCommentsTest()
        {
            service.AddComment(new Comment { Name = "Duri", Message = "asdasdascmkodkadad", TimeOfComment= DateTime.Now }) ;
            service.AddComment(new Comment{ Name = "Filip", Message = "aascmkodkadad", TimeOfComment = DateTime.Now });
            service.AddComment(new Comment{ Name = "Janko", Message = "auto", TimeOfComment = DateTime.Now });
            service.AddComment(new Comment{ Name = "Alex", Message = "koment", TimeOfComment = DateTime.Now });
            service.AddComment(new Comment{ Name = "Duri", Message = "daco" , TimeOfComment = DateTime.Now });

            var comment = service.GetLastComments();
            Assert.AreEqual<int>(3, comment.Count);

            Assert.AreEqual<string>("Duri", comment[0].Name);
            Assert.AreEqual<string>("daco", comment[0].Message);
            
            Assert.AreEqual<string>("Alex", comment[1].Name);
            Assert.AreEqual<string>("koment", comment[1].Message); 
            
            Assert.AreEqual<string>("Janko", comment[2].Name);
            Assert.AreEqual<string>("auto", comment[2].Message);

        }
    }
}