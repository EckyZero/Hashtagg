using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

		public static string UppercaseFirstCharacterRemainderUnchanged(this string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}

			return string.Format("{0}{1}",char.ToUpper(s[0]), s.Substring(1));
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

		public static string ToPhoneNumber(this string text)
		{
			Regex rgx = new Regex("[^a-zA-Z0-9 -]");
			var phone = String.IsNullOrWhiteSpace(text) ? string.Empty : rgx.Replace(text, "");

			phone = phone.Replace("-","");
			phone = phone.Replace("(","");
			phone = phone.Replace(")","");

			if(!String.IsNullOrWhiteSpace(text))
			{
				if(phone.Length > 10)
				{
					phone = String.Format("{0:(###) ###-#### x ####}", long.Parse(phone));	
				}
				else
				{
					phone = String.Format("{0:(###) ###-####}", long.Parse(phone));
				}
			}
			return phone; 
		}
	}
}

