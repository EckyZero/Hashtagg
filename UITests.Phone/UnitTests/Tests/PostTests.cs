using System;
using NUnit.Framework;
using System.Threading.Tasks;
using Shared.VM;

namespace UnitTests
{
    [TestFixture()]
    public class PostTests : BaseTest
    {
        [Test()]
        public async Task TestPost()
        {
            // Check default values
            var viewModel = new PostViewModel();

            await viewModel.DidLoad();

            Assert.True(viewModel.IsTwitterEnabled);
            Assert.True(viewModel.IsFacebookEnabled);
            Assert.AreEqual("0", viewModel.CharacterCount);
        }
    }
}

