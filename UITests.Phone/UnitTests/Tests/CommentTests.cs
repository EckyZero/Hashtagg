using System;
using NUnit.Framework;
using Shared.VM;
using System.Threading.Tasks;
using System.Linq;

namespace UnitTests
{
    [TestFixture()]
    public class CommentTests : BaseTest
    {
        [Test()]
        public async Task TestComments ()
        {
            // Test that the comment values are mapped correctly from the viewModel we pass in
            var posts = MockFacebook.GetPosts();
            var viewModel = new FacebookCardViewModel(posts[0]);
            var commentViewModel = new CommentViewModel(viewModel);

            await commentViewModel.DidLoad();

            // We do (+2) because there are always two additional more, the primary comment card and the "Now" header
            Assert.AreEqual(viewModel, commentViewModel.PrimaryCardViewModel);
            Assert.AreEqual(viewModel.CommentViewModels.Count + 2, commentViewModel.CardViewModels.Count);
        }
    }
}

