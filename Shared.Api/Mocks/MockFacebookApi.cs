using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Shared.Api
{
    public class MockFacebookApi : IFacebookApi
    {
        #region Variables

        IFileReader _reader;

        #endregion

        #region Constructors

        public MockFacebookApi ()
        {
            _reader = IocContainer.GetContainer().Resolve<IFileReader>();
        }

        #endregion

        #region Methods

        public Task<FacebookResponseDto> GetHomeFeed()
        {
            var results = _reader.ReadFile<FacebookResponseDto>("Facebook_Feed", "json");

            return Task.FromResult(results);
        }

        public Task<FacebookToFromDto> GetUser()
        {
            var results = _reader.ReadFile<FacebookToFromDto>("Facebook_User", "json");

            return Task.FromResult(results);
        }

        public Task Like(string postId)
        {
            return Task.FromResult<object>(null);
        }

        public Task Unlike(string postId)
        {
            return Task.FromResult<object>(null);
        }

        public Task Comment(string postId, string message)
        {
            return Task.FromResult<object>(null);
        }

        public Task DeleteComment(string commentId)
        {
            return Task.FromResult<object>(null);
        }

        public Task Post(string userId, string message)
        {
            return Task.FromResult<object>(null);
        }

        #endregion
    }
}

