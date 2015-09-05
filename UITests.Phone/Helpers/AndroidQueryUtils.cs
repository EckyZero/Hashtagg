using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;

namespace UITests.Phone
{
    class AndroidQueryUtils : IQueryUtils
    {
        public Func<AppQuery, AppQuery> LabelWithTextLike(string text)
        {
            return c => c.Property("text").Contains(text);
        }

        public Func<AppQuery, AppQuery> ButtonWithTextLike(string text)
        {
            return c => c.Property("text").Like("*" + text + "*");
        }

        public Func<AppQuery, AppQuery> ImageWithName(string name, string id)
        {
            return c => c.Id(id);
        }
    }
}
