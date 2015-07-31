using System.Linq;
using System.Text;

namespace Shared.Common
{
	public static class StringExtensions
	{
		public static string CreateSpaceBetweenCapitalLetters (this string s)
		{
			for (var i = 1; i < s.Length - 1; i++)
			{
				if (char.IsLower(s[i - 1]) && char.IsUpper(s[i]) ||
					s[i - 1] != ' ' && char.IsUpper(s[i]) && char.IsLower(s[i + 1]))
				{
					s = s.Insert(i, " ");
				}
			}
			return s;
		}

		public static string UppercaseFirstCharacter(this string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}

			return char.ToUpper(s[0]) + s.Substring(1).ToLower();
		}

		public static string CapitalizeWords(this string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}

			var words = s.Split (' ');

			var sb = new StringBuilder ();

			foreach(var word in words)
			{
				if(word.Length > 2){
					sb.Append (char.ToUpper(word[0]) + word.Substring(1).ToLower());
				}
				else{
					sb.Append (char.ToUpper(word[0]));
				}

				if(word != words.Last()){
					sb.Append (" ");
				}
			}

			return sb.ToString ();
		}

	}
}

