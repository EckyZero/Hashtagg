/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Shared"
                           x:Key="Locator" />
  </Application.Resources>
  

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

namespace Shared.VM
{
    public class ViewModelLocator
    {
		public const string HOME_KEY = "Home";

        public const string POST_KEY = "Post";

        public const string COMMENT_KEY = "Comment";
    }
}
