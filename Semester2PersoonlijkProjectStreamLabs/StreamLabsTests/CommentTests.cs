using Data.Interfaces;
using Data.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models;
using System;
using System.Linq;

namespace StreamLabsTests
{
    [TestClass]
    public class CommentTests
    {
        private CommentLogic _commentLogic;
        private ICommentContext _commentContext;
        public List<Comment> commentList = new List<Comment>();

        private void InstanceLogic()
        {
            commentList.Add(new Comment(1, 1, "testcomment", DateTime.Now));
            commentList.Add(new Comment(2, 1, "testcomment2", DateTime.Now));
            commentList.Add(new Comment(2, 1, "testcomment3", DateTime.Now));
            commentList.Add(new Comment(1, 2, "testcomment4", DateTime.Now));
            commentList.Add(new Comment(1, 2, "testcomment5", DateTime.Now));
            commentList.Add(new Comment(1, 2, "testcomment6", DateTime.Now));
            commentList.Add(new Comment(1, 1, "testcomment7", DateTime.Now));
            commentList.Add(new Comment(1, 1, "testcomment8", DateTime.Now));
            commentList.Add(new Comment(1, 1, "testcomment9", DateTime.Now));
            _commentContext = new CommentMemory(commentList);
            _commentLogic = new CommentLogic(_commentContext);
        }

        [TestMethod]
        public void GetAllCommentsByUserId()
        {
            //Arrange
            InstanceLogic();
            List<Comment> expectedList = new List<Comment>();
            foreach(var item in commentList.Where(x => x.UserId == commentList[1].UserId))
            {
                expectedList.Add(item);
            }

            var result = _commentLogic.GetAllCommentsByUser(commentList[1].UserId);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], result[i]);
            }
        }

        [TestMethod]
        public void GetAllCommentsByVideoId()
        {
            //Arrange
            InstanceLogic();
            List<Comment> expectedList = new List<Comment>();
            foreach (var item in commentList.Where(x => x.VideoId == commentList[2].VideoId))
            {
                expectedList.Add(item);
            }

            var result = _commentLogic.GetAllCommentsOnVideo(commentList[2].VideoId);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], result[i]);
            }
        }
    }
}