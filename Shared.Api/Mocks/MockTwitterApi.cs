using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;

namespace Shared.Api
{
    public class MockTwitterApi : ITwitterApi
    {
        #region Variables

        IFileReader _reader;
        JsonSerializerSettings _settings;

        #endregion

        #region Constructors

        public MockTwitterApi ()
        {
            _reader = IocContainer.GetContainer().Resolve<IFileReader>();
            _settings = new JsonSerializerSettings ();

            _settings.DateFormatString = "ddd MMM dd HH:mm:ss zzzz yyyy";
            _settings.Culture = CultureInfo.InvariantCulture;
        }

        #endregion

        #region Methods

        public Task<List<TwitterFeedItemDto>> GetHomeFeed()
        {
            var results = _reader.ReadFile<List<TwitterFeedItemDto>>("Twitter_Feed", "json", _settings);

            return Task.FromResult(results);
        }

        public Task<TwitterUserDto> GetUser(string screenName)
        {
            var results = _reader.ReadFile<TwitterUserDto>("Twitter_User", "json", _settings);

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

        public Task DeleteTweet(string tweetId)
        {
            return Task.FromResult<object>(null);
        }

        public Task Post(string message)
        {
            return Task.FromResult<object>(null);
        }

        #endregion
    }
}

