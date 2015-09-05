using NUnit.Framework;
using System;
using Shared.Common;
using System.Collections.Generic;
using Shared.VM;

namespace UnitTests
{
    [TestFixture()]
    public class HomeTests : BaseTest
    {
        [Test()]
        public void TestFacebookCard()
        {
            // Test that Facebook values are mapped correctly
            var posts = MockFacebook.GetPosts();
            var viewModels = new List<BaseContentCardViewModel>();

            foreach(FacebookPost p in posts)
            {
                viewModels.Add(new FacebookCardViewModel(p));
            }
                
            var viewModel = viewModels[0];
            var post = posts[0];

            Assert.AreEqual(viewModel.SocialMediaImage, "Facebook.png");
            Assert.AreEqual(viewModel.CommentCount.Value, post.Comments.Count);
            Assert.AreEqual(viewModel.ShareCount.Value, post.ShareCount);
        }

        [Test()]
        public void TestTwitterCard()
        {
            // Test that Twitter values are mapped correctly
            var tweets = MockTwitter.GetPosts();
            var viewModels = new List<BaseContentCardViewModel>();

            foreach(Tweet t in tweets)
            {
                viewModels.Add(new TwitterCardViewModel(t));
            }

            var viewModel = viewModels[0];
            var tweet = tweets[0];

            Assert.AreEqual(viewModel.SocialMediaImage, "Twitter.png");
            Assert.AreEqual(viewModel.IsLikedByUser, tweet.IsFavoritedByUser);
            Assert.AreEqual(viewModel.IsCommentedByUser, tweet.IsRetweetedByUser);
            Assert.AreEqual(viewModel.ShareCount.Value, tweet.RetweetCount);
        }
    }
}

